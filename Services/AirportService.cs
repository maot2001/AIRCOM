using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class AirportService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly InstallationService _aux;
        public AirportService(DBContext context, IMapper mapper, InstallationService aux)
        {
            _context = context;
            _mapper = mapper;
            _aux = aux;
        }

        public async Task<IEnumerable<AirportDTO>> Get()
        {
            var airports = await _context.Airports.ToListAsync();
            return _mapper.Map<List<AirportDTO>>(airports);
        }

        public async Task<int> Create(AirportDTO airport)
        {
            var airportDB = _mapper.Map<Airport>(airport);

            _context.Airports.Add(airportDB);
            await _context.SaveChangesAsync();

            var airp = await _context.Airports.FirstOrDefaultAsync(a => a.Name == airport.Name);
            return airp.AirportID;
        }

        /*public async Task Edit(AirportDTO airport)
        {
            var airportDB = await GetAirportById(airport.AirportID);

            airportDB.Name = airport.Name;
            airportDB.Coordinates = airport.Coordinates;
            airportDB.Direction = airport.Direction;

            await _context.SaveChangesAsync();
        }*/

        public async Task Delete(string name, bool all)
        {
            var airportDB = await GetAirportByName(name);

            bool money = false;
            foreach (var inst in airportDB.Installations)
            {
                if (money) break;
                foreach (var service in inst.ServicesInstallations)
                {
                    if (money) break;
                    foreach (var i in service.On_Sites)
                    {
                        if (money) break;
                        money = true;
                    }
                }
                foreach (var repair in inst.RepairInstallations)
                {
                    if (money) break;
                    foreach (var i in repair.RepairShips)
                    {
                        if (money) break;
                        money = true;
                    }
                }
            }

            await Waterfall(airportDB, !money);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------

        private async Task Waterfall(Airport airport, bool delete)
        {
            foreach(var i in airport.Installations)
            {
                await _aux.Waterfall(i, delete);
            }
            foreach (var w in airport.Workers)
            {
                if (delete)
                    _context.Workers.Remove(w);
                else
                    w.Active = false;
            }
             if(delete)
                _context.Airports.Remove(airport);
             else
                airport.Active = false;
        }

        private async Task<Airport> GetAirportByName(string name)
        {
            var airportDB = await _context.Airports
                .Include(a => a.Installations).ThenInclude(i => i.RepairInstallations).ThenInclude(ri => ri.RepairShips)
                .Include(a => a.Installations).ThenInclude(i => i.ServicesInstallations).ThenInclude(si => si.On_Sites)
                .Include(a => a.Workers)
                .SingleOrDefaultAsync(a => a.Name == name && a.Active == true);

            if (airportDB is null)
                throw new Exception("El aeropuerto no existe");

            return airportDB;
        }
    }
}
