﻿using AIRCOM.Models;
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

        public async Task<IEnumerable<ShipsDTO>> Get(string? userId = null)
        {
            List<Ships> ships;

            if (userId is null)
                ships = await _context.Shipss
                    .Include(s => s.Reports)
                    .Include(s => s.Histories)
                    .ToListAsync();

            else
                ships = await _context.Shipss
                    .Include(s => s.Reports)
                    .Include(s => s.Histories)
                    .Where(s => s.ClientID == int.Parse(userId)).ToListAsync();
            
            foreach (var ship in ships)
            {
                if (ship.NextFly < DateTime.Now)
                {
                    bool repair = false;
                    foreach (var report in ship.Reports)
                    {
                        if (report.Init is not null && report.Finish is null)
                            repair = true;
                    }
                    if (!repair)
                        ship.NextFly = GetMinimumDateTime(ship.Histories);
                }
            }

            return _mapper.Map<List<ShipsDTO>>(ships);
        }

        public async Task Create(ShipsDTO ship)
        {
            var ships = _mapper.Map<Ships>(ship);

            if (ship.ClientID is not null)
                await Errors(ship.ClientID);

            _context.Shipss.Add(ships);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ShipsDTO ship)
        {
            var shipBD = await GetShipById(ship.Plate);

            if (ship.ClientID != 0)
                await Errors(ship.ClientID);

            shipBD.ClientID = ship.ClientID;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var ship = await GetShipById(id);

            if (ship.Reports.Count == 0)
                _context.Shipss.Remove(ship);
            else
            {
                ship.Active = false;
                ship.ClientID = 0;
            }

            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------
        private async Task<Ships> GetShipById(string id)
        {
            var ship = await _context.Shipss
                .Include(s => s.Reports)
                .SingleOrDefaultAsync(s => s.Plate == id);

            if (ship is null)
                throw new Exception("El avión no existe");

            return ship;
        }

        private async Task Errors(int? id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client is null)
                throw new Exception("El cliente no existe");
        }

        private DateTime? GetMinimumDateTime (IEnumerable<History>? histories)
        {
            IEnumerable<History> dates = histories.Where(h => h.ExitDate > DateTime.Now);
            DateTime? minimumDateTime = DateTime.MaxValue;

            foreach (History date in dates)
            {
                if (date.ExitDate < minimumDateTime)
                {
                    minimumDateTime = date.ExitDate;
                }
            }
            return minimumDateTime;
        }
    }
}
