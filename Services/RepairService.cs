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
            var repairDB = _mapper.Map<Repair>(repair);

            _context.Repairs.Add(repairDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(RepairDTO repair)
        {
            var repairDB = await GetRepairById(repair.RepairID);

            repairDB.Name = repair.Name;
            repairDB.Description = repair.Description;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int repairId)
        {
            var repairDB = await GetRepairById(repairId);

            _context.Repairs.Remove(repairDB);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------

        private async Task<Repair> GetRepairById(int? repairId)
        {
            var repairDB = await _context.Repairs.FindAsync(repairId);

            if (repairDB is null)
                throw new Exception("La reparación no existe");

            return repairDB;
        }

    }
}
