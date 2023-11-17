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

        public async Task Create(AirportDTO airport)
        {
            //await Errors(airport);
            var airportDB = _mapper.Map<Airport>(airport);
            airportDB.AirportID = 0;
            _context.Airports.Add(airportDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(AirportDTO airport)
        {
            //await Errors(airport);
            var airportDB = await GetAirportById(airport.AirportID);
            airportDB.Name = airport.Name;
            airportDB.Coordinates = airport.Coordinates;
            airportDB.Direction = airport.Direction;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int airportId)
        {
            var airportDB = await GetAirportById(airportId);
            _context.Airports.Remove(airportDB);
            await _context.SaveChangesAsync();
        }

        // --------------------------------------------------------------
        /*private async Task Errors(AirportDTO airport)
        {
            var airportDB = await _context.Airports.SingleOrDefaultAsync(a => 
            a.Coordinates == airport.Coordinates || a.Direction == airport.Direction);
            if (airportDB is not null)
                throw new Exception();
        }*/

        private async Task<Airport> GetAirportById(int airportId)
        {
            var airportDB = await _context.Airports.FindAsync(airportId);
            if (airportDB is null)
                throw new Exception();
            return airportDB;
        }
    }
}
