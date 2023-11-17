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

        public async Task<IEnumerable<RepairInstallation>> Get(string userId)
        {
            int id = int.Parse(userId);
            return await _context.RepairInstallations.Where(ri => ri.AirportID == id).ToListAsync();
        }

        public async Task Create(RepairInstallationDTO repair, string userId)
        {
            await Errors(repair, userId);
            repair.AirportID = int.Parse(userId);
            var repairInstallation = _mapper.Map<RepairInstallation>(repair);
            _context.RepairInstallations.Add(repairInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(RepairInstallationDTO repair, string userId)
        {
            await Errors(repair, userId);
            var repairDB = await GetRepairInstallation(repair);
            repairDB.InstallationID = repair.InstallationID;
            repairDB.Name = repair.Name;
            repairDB.Price = repair.Price;
            repairDB.RepairID = repair.RepairID;
            repairDB.Repair = await _context.Repairs.FindAsync(repair.RepairID);
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
                throw new Exception();
            return repairDB;
        }

        private async Task Errors(RepairInstallationDTO repair, string userId)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.InstallationID == repair.InstallationID && i.AirportID == repair.AirportID);
            var repairDB = await _context.Repairs.SingleOrDefaultAsync(r => r.RepairID == repair.RepairID);
            if (repairDB is null || installationDB is null)
                throw new Exception();

            var exist = _context.RepairInstallations.SingleOrDefaultAsync(ri =>
            ri.InstallationID == repair.InstallationID && ri.AirportID == int.Parse(userId) && ri.RepairID == repair.RepairID);
            if (exist is not null)
                throw new Exception();
        }
    }
}
