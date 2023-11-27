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

        public async Task Create(ServiceInstallationDTO service, string userId)
        {
            await Errors(service, userId);
            var serviceInstallation = _mapper.Map<ServicesInstallation>(service);
            _context.ServicesInstallations.Add(serviceInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ServiceInstallationDTO service, string userId)
        {
            await Errors(service, userId);
            var serviceDB = await GetServiceInstallation(service);
            serviceDB.InstallationID = service.InstallationID;
            serviceDB.Name = service.Name;
            serviceDB.Price = service.Price;
            serviceDB.Code = service.Code;
            serviceDB.CustomerService = await _context.CustomerServices.FindAsync(service.Code);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ServiceInstallationDTO service)
        {
            var serviceDB = await GetServiceInstallation(service);
            _context.ServicesInstallations.Remove(serviceDB);
            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        public async Task<ServicesInstallation> GetServiceInstallation(ServiceInstallationDTO service)
        {
            var serviceDB = await _context.ServicesInstallations.SingleOrDefaultAsync(si =>
            si.InstallationID == service.InstallationID && si.Code == service.Code);
            if (serviceDB is null)
                throw new Exception();
            return serviceDB;
        }

        private async Task Errors(ServiceInstallationDTO service, string userId)
        {
            var installationDB = await _context.Installations.SingleOrDefaultAsync(i =>
            i.InstallationID == service.InstallationID);
            var serviceDB = await _context.Repairs.SingleOrDefaultAsync(r => r.RepairID == service.Code);
            if (serviceDB is null || installationDB is null)
                throw new Exception();

            var exist = _context.RepairInstallations.SingleOrDefaultAsync(si =>
            si.InstallationID == service.InstallationID && si.Installation.AirportID == int.Parse(userId) && si.RepairID == service.Code);
            if (exist is not null)
                throw new Exception();
        }
    }
}
