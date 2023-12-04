using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ServiceInstallationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ServiceInstallationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceInstallationDTO>> Get(string? userId = null, int? id = null)
        {
            List<ServicesInstallation> service;
            List<ServiceInstallationDTO> result = new();
            if (id is not null)
                service = await _context.ServicesInstallations.Where(si => si.InstallationID == id).ToListAsync();
            else if (userId is null)
            {
                service = await _context.ServicesInstallations.Include(si => si.Installation).ToListAsync();
            }
            else
                service = await _context.ServicesInstallations
                    .Include(si => si.Installation)
                    .Where(si => si.Installation.AirportID == int.Parse(userId)).ToListAsync();
                result = _mapper.Map<List<ServiceInstallationDTO>>(service);
            foreach (var serv in result)
            {
                var installation = await _context.Installations.FindAsync(serv.InstallationID);
                serv.InstallationID = installation.InstallationID;
                serv.AirportID = installation.AirportID;
            }
            return result;
        }

        public async Task<string> ObtAirport(string? userId)
        {
            var airport = await _context.Airports.FindAsync(int.Parse(userId));
            return airport.Name;
        }

        public async Task<ServiceInstallationDTO> GetComments(string? userId, int? code)
        {
            var inst = await _context.ServicesInstallations.Include(si => si.On_Sites).SingleOrDefaultAsync(si => si.Code == code);
            var result = _mapper.Map<ServiceInstallationDTO>(inst);
            return result;
        }

        public async Task Create(ServiceInstallationDTO service)
        {
            await Errors(service);
            var serviceInstallation = _mapper.Map<ServicesInstallation>(service);
            _context.ServicesInstallations.Add(serviceInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ServiceInstallationDTO service)
        {
            await Errors(service);
            var serviceDB = await GetServiceInstallation(service);
            serviceDB.Price = service.Price;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ServiceInstallationDTO service)
        {
            var serviceDB = await GetServiceInstallation(service);

            if (serviceDB.On_Sites.Count == 0)
                _context.ServicesInstallations.Remove(serviceDB);
            else
                serviceDB.Active = false;

            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        public async Task<ServicesInstallation> GetServiceInstallation(ServiceInstallationDTO service)
        {
            var serviceDB = await _context.ServicesInstallations
                .Include(si => si.On_Sites)
                .SingleOrDefaultAsync(si => si.InstallationID == service.InstallationID && si.Code == service.Code);

            if (serviceDB is null)
                throw new Exception();
            return serviceDB;
        }

        private async Task Errors(ServiceInstallationDTO service)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.ID == service.InstallationID);
            if (installationDB is null)
                throw new Exception();

            var exist = await _context.ServicesInstallations.SingleOrDefaultAsync(si =>
            si.InstallationID == service.InstallationID && si.Name == service.Name);
            if (exist is not null)
                throw new Exception();
        }
    }
}
