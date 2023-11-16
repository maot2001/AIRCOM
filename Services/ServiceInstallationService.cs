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

        public async Task<IEnumerable<ServicesInstallation>> Get(string userId)
        {
            int id = int.Parse(userId);
            return await _context.ServicesInstallations.Where(ri => ri.AirportID == id).ToListAsync();
        }

        public async Task Create(ServiceInstallationDTO service)
        {
            Installation installation = new() { InstallationID = service.InstallationID, AirportID = service.AirportID };
            InstallationService aux = null;
            var installationDB = await aux.GetInstallation(installation);
            var serviceDB = await _context.CustomerServices.SingleOrDefaultAsync(s => s.Code == service.Code);
            if (serviceDB is null)
                throw new Exception();

            ServicesInstallation serviceInstallation = new()
            { InstallationID = service.InstallationID, AirportID = service.AirportID, Code = service.Code};
            _context.ServicesInstallations.Add(serviceInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ServiceInstallationDTO service)
        {
            var serviceDB = await GetServiceInstallation(service);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ServiceInstallationDTO service)
        {
            var serviceDB = await GetServiceInstallation(service);
            _context.ServicesInstallations.Remove(serviceDB);
            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        private async Task<ServicesInstallation> GetServiceInstallation(ServiceInstallationDTO service)
        {
            var serviceDB = await _context.ServicesInstallations.SingleOrDefaultAsync(si =>
            si.InstallationID == service.InstallationID && si.AirportID == service.AirportID && si.Code == service.Code);
            if (serviceDB is null)
                throw new Exception();
            return serviceDB;
        }

    }
}
