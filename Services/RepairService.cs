using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class RepairService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public RepairService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RepairDTO>> Get()
        {
            var repairs = await _context.Repairs.ToListAsync();
            return _mapper.Map<List<RepairDTO>>(repairs);
        }

        public async Task Create(RepairDTO repair)
        {
            var exist = await _context.Repairs.SingleOrDefaultAsync(r => r.Name == repair.Name);
            if (exist is not null)
                exist.Description = repair.Description;
            else
            {
                var repairDB = _mapper.Map<Repair>(repair);
                _context.Repairs.Add(repairDB);
            }
            await _context.SaveChangesAsync();
        }

        /*public async Task Edit(RepairDTO repair)
        {
            var repairDB = await GetRepairById(repair.RepairID);

            repairDB.Name = repair.Name;
            repairDB.Description = repair.Description;

            await _context.SaveChangesAsync();
        }*/

        public async Task Delete(string name, bool all)
        {
            var repairDB = await GetRepairByName(name);

            _context.Repairs.Remove(repairDB);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------

        private async Task<Repair> GetRepairByName(string name)
        {
            var repairDB = await _context.Repairs.SingleOrDefaultAsync(r => r.Name == name);

            if (repairDB is null)
                throw new Exception("La reparación no existe");

            return repairDB;
        }

    }
}
