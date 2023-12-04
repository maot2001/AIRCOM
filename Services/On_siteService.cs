using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class On_siteService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly ServiceInstallationService _aux;
        public On_siteService(DBContext context, IMapper mapper, ServiceInstallationService aux)
        {
            _context = context;
            _mapper = mapper;
            _aux = aux;
        }

        public async Task<On_siteDTO> GetOne(int? code, string? Id)
        {
            var service = await _context.On_Site.FirstOrDefaultAsync(o => o.Code == code && o.ClientID == int.Parse(Id));
            return _mapper.Map<On_siteDTO>(service);
        }

        public async Task<int> GetAir(int? code)
        {
            var service = await _context.On_Site.Include(o => o.ServiceInstallation).ThenInclude(si => si.Installation)
                .SingleOrDefaultAsync(o => o.Code == code);
            return service.ServiceInstallation.Installation.AirportID;
        }

        public async Task<IEnumerable<On_siteDTO>> Get(string userId)
        {
            var service = await _context.On_Site.Where(s => s.ClientID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<On_siteDTO>>(service);
        }

        public async Task Create(ServiceInstallationDTO service, string userId)
        {
            var serviceDB = _mapper.Map<on_site>(service);
            serviceDB.ClientID = int.Parse(userId);
            serviceDB.Fecha = DateTime.Now;
            _context.On_Site.Add(serviceDB);
            await _context.SaveChangesAsync();
        }

        public async Task Valorate(On_siteDTO service)
        {
            var on_site = await _context.On_Site.Include(o => o.ServiceInstallation).SingleOrDefaultAsync(o => o.ID == service.ID);
            if (on_site.Fecha.ToString().Split()[0] != service.Fecha.ToString().Split()[0])
                throw new Exception("Usted no recibio un servicio este dia");

            on_site.Stars = service.Stars;
            on_site.Comment = service.Comment;

            var serviceDB = await _aux.GetServiceInstallation(_mapper.Map<ServiceInstallationDTO>(on_site.ServiceInstallation));
            if (serviceDB.Stars is null)
                serviceDB.Stars = 0;
            if (serviceDB.Votes is null)
                serviceDB.Votes = 0;
            var prom = serviceDB.Stars * (float)serviceDB.Votes;
            prom += on_site.Stars;
            serviceDB.Votes++;
            prom /= serviceDB.Votes;
            serviceDB.Stars = prom;

            await _context.SaveChangesAsync();
        }
        // --------------------------------------------------------
    }
}
