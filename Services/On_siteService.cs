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

        public async Task<IEnumerable<On_siteDTO>> Get(string userId)
        {
            var service = await _context.On_Site.Where(s => s.ClientID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<On_siteDTO>>(service);
        }

        public async Task Create(ServiceInstallationDTO service, string userId)
        {
            var serviceDB = _mapper.Map<on_site>(service);
            serviceDB.Name = service.Name;
            serviceDB.ClientID = int.Parse(userId);
            serviceDB.Client = await _context.Clients.FindAsync(int.Parse(userId));
            if (serviceDB.Fecha == default(DateTime))
                serviceDB.Fecha = DateTime.Now;
            _context.On_Site.Add(serviceDB);
            await _context.SaveChangesAsync();
        }

        public async Task Valorate(On_siteDTO service)
        {
            var on_site = await _context.On_Site.FindAsync(service.Id);
            on_site.Stars = service.Stars;
            on_site.Comment = service.Comment;

            var serviceDB = await _aux.GetServiceInstallation(_mapper.Map<ServiceInstallationDTO>(service));
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
