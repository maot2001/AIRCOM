using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ShipsService
    {
        private readonly DBContext _context;
        public ShipsService(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ships>> Get()
        {
            return await _context.Ships.ToListAsync();
        }

        public async Task Create(Ships ship)
        {
            _context.Ships.Add(ship);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(string id, Ships ship)
        {
            var shipBD = await GetShipById(id);
            shipBD.Model = ship.Model;
            shipBD.Capacity = ship.Capacity;
            shipBD.Crew = ship.Crew;
            shipBD.ClientID = ship.ClientID;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var ship = await GetShipById(id);
            _context.Ships.Remove(ship);
            _context.SaveChanges();
        }

        // -----------------------------------------------------------
        private async Task<Ships> GetShipById(string id)
        {
            var ship = await _context.Ships.FindAsync(id);
            if (ship is null)
                throw new Exception();
            return ship;
        }
    }
}
