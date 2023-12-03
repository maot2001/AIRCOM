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

        public async Task<IEnumerable<ServiceInstallationDTO>> Get(string? userId = null)
        {
            List<ServicesInstallation> service;
            if (userId is null)
                service = await _context.ServicesInstallations.ToListAsync();
            else
                service = await _context.ServicesInstallations.Where(si => si.Installation.AirportID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<ServiceInstallationDTO>>(service);
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
            serviceDB.InstallationID = service.InstallationID;
            serviceDB.Name = service.Name;
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

            var exist = _context.ServicesInstallations.SingleOrDefaultAsync(si =>
            si.InstallationID == service.InstallationID && si.Name == service.Name);
            if (exist is not null)
                throw new Exception();
        }
    }
}
