using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class DependService
    {
        private readonly DBContext _context;
        public DependService(DBContext context)
        {
            _context = context;
        }

        public async Task Create(Depend depend)
        {
            if (depend.PrimaryID == depend.SecondID)
                throw new Exception("La dependencia no puede ser creada");

            var dependDB = await _context.Depends
                .SingleOrDefaultAsync(d => d.PrimaryID == depend.PrimaryID && d.SecondID == depend.SecondID && d.State == depend.State);
            if (dependDB is not null)
                throw new Exception("La dependencia ya existe");

            _context.Depends.Add(depend);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Depend depend)
        {
            var dependDB = await _context.Depends
                .SingleOrDefaultAsync(d => d.PrimaryID == depend.PrimaryID && d.SecondID == depend.SecondID && d.State == depend.State);
            if (dependDB is null)
                throw new Exception("La dependencia no existe");

            _context.Depends.Remove(depend);
            await _context.SaveChangesAsync();
        }
    }
}
