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
        private readonly InstallationService _aux;
        public RepairInstallationService(DBContext context, IMapper mapper, InstallationService aux)
        {
            _context = context;
            _mapper = mapper;
            _aux = aux;
        }

        public async Task<IEnumerable<RepairInstallationDTO>> Get(string userId)
        {
            var repairsDB = await _context.RepairInstallations.Where(ri => ri.Installation.AirportID == int.Parse(userId)).ToListAsync();
            var repairs = _mapper.Map<List<RepairInstallationDTO>>(repairsDB);
            foreach (var repair in repairs)
            {
                var installation = await _aux.GetInstallationById(repair.InstallationID);
                repair.InstallationID = installation.InstallationID;
            }
            return repairs;
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
            //epairDB.Price = repair.Price;

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
            var installationDB = await _aux.GetInstallationById(repair.InstallationID);
            
            var repairDB = await _context.RepairInstallations.SingleOrDefaultAsync(ri => 
            ri.InstallationID == installationDB.InstallationID && ri.AirportID == installationDB.AirportID && ri.RepairID == repair.RepairID);

            if (repairDB is null)
                throw new Exception("En esta instalación no se realiza esta reparación");

            return repairDB;
        }

        private async Task Errors(RepairInstallationDTO repair, string userId)
        {
            var installationDB = await _aux.GetInstallationById(repair.InstallationID);

            var repairDB = await _context.Repairs.SingleOrDefaultAsync(r => r.RepairID == repair.RepairID);
            if (repairDB is null)
                throw new Exception("La reparación no existe");

            var exist = await _context.RepairInstallations.SingleOrDefaultAsync(ri =>
            ri.InstallationID == installationDB.ID && ri.AirportID == installationDB.AirportID && ri.RepairID == repair.RepairID);
            if (exist is not null)
                throw new Exception("En esta instalación ya se realiza esta reparación");

            repair.Name = repairDB.Name;
            repair.AirportID = installationDB.AirportID;
        }
    }
}
