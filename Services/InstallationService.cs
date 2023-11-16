using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class InstallationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public InstallationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Installation>> Get(string userId)
        {
            int id = int.Parse(userId);
            return await _context.Installations.Where(i => i.AirportID == id).ToListAsync();
        }

        public async Task Create(Installation intallation, string userId)
        {
            _context.Installations.Add(intallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Installation installation)
        {
            var installationDB = await GetInstallation(installation);
            installationDB.Name = installation.Name;
            installationDB.Direction = installation.Direction;
            installationDB.Ubication = installation.Ubication;
            
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Installation installation)
        {
            var installationDB = await GetInstallation(installation);
            _context.Installations.Remove(installationDB);
            await _context.SaveChangesAsync();
        }

        //--------------------------------------------------------------------
        public async Task<Installation> GetInstallation(Installation installation)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.InstallationID == installation.InstallationID && i.AirportID == installation.AirportID);

            if (installationDB is null)
                throw new Exception();
            return installationDB;
        }
    }
}
