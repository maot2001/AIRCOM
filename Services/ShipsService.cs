using AIRCOM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ShipsService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ShipsService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Ships>> Get()
        {
            return await _context.Shipss.ToListAsync();
        }

        public async Task Create(Ships ship)
        {
            _context.Shipss.Add(ship);
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
            _context.Shipss.Remove(ship);
            _context.SaveChanges();
        }

        // -----------------------------------------------------------
        private async Task<Ships> GetShipById(string id)
        {
            var ship = await _context.Shipss.FindAsync(id);
            if (ship is null)
                throw new Exception();
            return ship;
        }
    }
}
