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
            if (!all)
                airportDB.Active = false;
            /*else
            {

            }*/

            _context.Airports.Remove(airportDB);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------

        private async Task<Airport> GetAirportByName(string name)
        {
            var airportDB = await _context.Airports.SingleOrDefaultAsync(a => a.Name == name && a.Active == true);

            if (airportDB is null)
                throw new Exception("El aeropuerto no existe");

            return airportDB;
        }
    }
}
