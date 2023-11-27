using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class RepairInstallationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public RepairInstallationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RepairInstallationDTO>> Get(string userId)
        {
            var repair = await _context.RepairInstallations.Where(ri => ri.Installation.AirportID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<RepairInstallationDTO>>(repair);
        }

        public async Task Create(RepairInstallationDTO repair, string userId)
        {
            await Errors(repair, userId);

            var repairInstallation = _mapper.Map<RepairInstallation>(repair);

            _context.RepairInstallations.Add(repairInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(RepairInstallationDTO repair, string userId)
        {
            await Errors(repair, userId);

            var repairDB = await GetRepairInstallation(repair);
            repairDB.Price = repair.Price;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(RepairInstallationDTO repair)
        {
            var repairDB = await GetRepairInstallation(repair);

            _context.RepairInstallations.Remove(repairDB);
            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        public async Task<RepairInstallation> GetRepairInstallation(RepairInstallationDTO repair)
        {
            var repairDB = await _context.RepairInstallations.SingleOrDefaultAsync(ri => 
            ri.InstallationID == repair.InstallationID && ri.AirportID == repair.AirportID && ri.RepairID == repair.RepairID);

            if (repairDB is null)
                throw new Exception("En esta instalación no se realiza esta reparación");

            return repairDB;
        }

        private async Task Errors(RepairInstallationDTO repair, string userId)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i => 
            i.InstallationID == repair.InstallationID && i.AirportID == int.Parse(userId));
            if (installationDB is null)
                throw new Exception("La instalación no existe");

            var repairDB = await _context.Repairs.SingleOrDefaultAsync(r => r.RepairID == repair.RepairID);
            if (repairDB is null)
                throw new Exception("La reparación no existe");

            var exist = await _context.RepairInstallations.SingleOrDefaultAsync(ri =>
            ri.InstallationID == repair.InstallationID && ri.AirportID == int.Parse(userId) && ri.RepairID == repair.RepairID);
            if (exist is not null)
                throw new Exception("En esta instalación ya se realiza esta reparación");
        }
    }
}
