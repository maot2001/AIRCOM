using AIRCOM.Models;
using AIRCOM.Models.DTO;
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

        public async Task<IEnumerable<ShipsDTO>> Get(string userId = "")
        {
            List<Ships> ships;
            if (userId == "")
                ships = await _context.Shipss.ToListAsync();
            else
                ships = await _context.Shipss.Where(s => s.ClientID == int.Parse(userId)).ToListAsync();
            return _mapper.Map<List<ShipsDTO>>(ships);
        }

        public async Task Create(ShipsDTO ship)
        {
            var ships = _mapper.Map<Ships>(ship);
            if (ship.ClientID != 0)
            {
                var client = await Errors(ship.ClientID);
                ships.Clients = client;
            }
            _context.Shipss.Add(ships);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ShipsDTO ship)
        {
            var shipBD = await GetShipById(ship.Plate);
            if (ship.ClientID != 0)
            {
                var client = await Errors(ship.ClientID);
                shipBD.Clients = client;
            }
            shipBD.Model = ship.Model;
            shipBD.Capacity = ship.Capacity;
            shipBD.Crew = ship.Crew;
            shipBD.ClientID = ship.ClientID;
            shipBD.State = ship.State;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var ship = await GetShipById(id);
            _context.Shipss.Remove(ship);
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------
        private async Task<Ships> GetShipById(string id)
        {
            var ship = await _context.Shipss.FindAsync(id);
            if (ship is null)
                throw new Exception();
            return ship;
        }

        private async Task<Client> Errors(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client is null)
                throw new Exception();
            return client;
        }
    }
}
