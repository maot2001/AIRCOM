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
        public AirportService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            if (airportDB.On_Sites.Count == 0 && airportDB.RepairShip.Count == 0)
                await Waterfall(airportDB, true);
            else
                await Waterfall(airportDB, false);

            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------

        private async Task Waterfall(Airport airport, bool delete)
        {
            foreach(var ri in airport.RepairInstallation)
            {
                if(delete)
                    _context.RepairInstallations.Remove(ri);
                else
                    ri.Active = false;
            }
            foreach (var si in airport.ServicesInstallations)
            {
                if (delete)
                    _context.ServicesInstallations.Remove(si);
                else
                    si.Active = false;
            }
            foreach (var i in airport.Installations)
            {
                if (delete)
                    _context.Installations.Remove(i);
                else
                    i.Active = false;
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
                .Include(a => a.Installations)
                .Include(a => a.ServicesInstallations)
                .Include(a => a.On_Sites)
                .Include(a => a.RepairInstallation)
                .Include(a => a.RepairShip)
                .Include(a => a.Workers)
                .SingleOrDefaultAsync(a => a.Name == name && a.Active == true);

            if (airportDB is null)
                throw new Exception("El aeropuerto no existe");

            return airportDB;
        }
    }
}
