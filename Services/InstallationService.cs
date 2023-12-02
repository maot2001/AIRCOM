using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IEnumerable<InstallationDTO>> Get(string userId)
        {
            var installation = await _context.Installations.Where(i => i.AirportID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<InstallationDTO>>(installation);
        }

        public async Task<SelectList> Select(string userId)
        {
            var installations = await Get(userId);
            return new SelectList(installations, "ID", "Name");
        }

        public async Task Create(InstallationDTO installation, string userId)
        {
            await Errors(installation, userId);

            var installationDB = _mapper.Map<Installation>(installation);

            installationDB.AirportID = int.Parse(userId);

            _context.Installations.Add(installationDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(InstallationDTO installation)
        {
            await Errors(installation, installation.AirportID.ToString());

            var installationDB = await GetInstallationById(installation.ID);

            installationDB.Name = installation.Name;
            installationDB.Ubication = installation.Ubication;
            installationDB.InstallationID = installation.InstallationID;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var installationDB = await GetInstallationById(id);

            bool money = false;
            foreach (var service in installationDB.ServicesInstallations)
            {
                if (money) break;
                foreach (var i in service.On_Sites)
                {
                    if (money) break;
                    money = true;
                }
            }
            foreach (var repair in installationDB.RepairInstallations)
            {
                if (money) break;
                foreach (var i in repair.RepairShips)
                {
                    if (money) break;
                    money = true;
                }
            }

            await Waterfall(installationDB, !money);
            await _context.SaveChangesAsync();
        }

        //--------------------------------------------------------------------
        public async Task<Installation> GetInstallationById(int? id)
        {
            var installationDB = await _context.Installations
                .Include(i => i.RepairInstallations).ThenInclude(ri => ri.RepairShips)
                .Include(i => i.ServicesInstallations).ThenInclude(si => si.On_Sites)
                .SingleOrDefaultAsync(i => i.ID == id);

            if (installationDB is null)
                throw new ArgumentNullException("La instalación no existe");

            return installationDB;
        }

        public async Task Waterfall(Installation installation, bool delete)
        {
            foreach (var ri in installation.RepairInstallations)
            {
                if (delete || ri.RepairShips.Count == 0)
                    _context.RepairInstallations.Remove(ri);
                else
                    ri.Active = false;
            }
            foreach (var si in installation.ServicesInstallations)
            {
                if (delete || si.On_Sites.Count == 0)
                    _context.ServicesInstallations.Remove(si);
                else
                    si.Active = false;
            }
            if (delete)
                _context.Installations.Remove(installation);
            else
                installation.Active = false;
        }

        private async Task Errors(InstallationDTO installation, string userId)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.InstallationID == installation.InstallationID && i.AirportID == int.Parse(userId));

            if (installationDB is not null)
                throw new Exception("Este número de instalación ya existe en el aeropuerto");

            installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.Name == installation.Name && i.AirportID == int.Parse(userId));

            if (installationDB is not null)
                throw new Exception("Este nombre de instalación ya existe en el aeropuerto");
        }
    }
}
