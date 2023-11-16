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

        public async Task Create(RepairInstallationDTO repair)
        {
            Installation installation = new() { InstallationID = repair.InstallationID, AirportID = repair.AirportID };
            InstallationService aux = null;
            var installationDB = await aux.GetInstallation(installation);
            var repairDB = await _context.Repairs.SingleOrDefaultAsync(r => r.RepairID == repair.RepairID);
            if (repairDB is null)
                throw new Exception();

            RepairInstallation repairInstallation = new()
            { InstallationID = repair.InstallationID, AirportID = repair.AirportID, RepairID = repair.RepairID };
            _context.RepairInstallations.Add(repairInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(RepairInstallationDTO repair)
        {
            var repairDB = await GetRepairInstallation(repair);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(RepairInstallationDTO repair)
        {
            var repairDB = await GetRepairInstallation(repair);
            _context.RepairInstallations.Remove(repairDB);
            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        private async Task<RepairInstallation> GetRepairInstallation(RepairInstallationDTO repair)
        {
            var repairDB = await _context.RepairInstallations.SingleOrDefaultAsync(ri =>
            ri.InstallationID == repair.InstallationID && ri.AirportID == repair.AirportID && ri.RepairID == repair.RepairID);
            if (repairDB is null)
                throw new Exception();
            return repairDB;
        }
    }
}
