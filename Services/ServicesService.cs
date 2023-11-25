using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ServicesService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ServicesService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDTO>> Get()
        {
            var service = await _context.CustomerServices.ToListAsync();
            return _mapper.Map<List<ServiceDTO>>(service);
        }

        public async Task Create(ServiceDTO service)
        {
            var serviceDB = _mapper.Map<CustomerService>(service);

            _context.CustomerServices.Add(serviceDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ServiceDTO service)
        {
            var serviceDB = await GetServiceById(service.Code);

            serviceDB.Name = service.Name;
            serviceDB.Description = service.Description;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int serviceId)
        {
            var serviceDB = await GetServiceById(serviceId);

            _context.CustomerServices.Remove(serviceDB);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------
        private async Task<CustomerService> GetServiceById(int? serviceId)
        {
            var serviceDB = await _context.CustomerServices.FindAsync(serviceId);

            if (serviceDB is null)
                throw new Exception("El servicio no existe");

            return serviceDB;
        }

    }
}
