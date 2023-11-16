using AIRCOM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class HistoryService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public HistoryService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<History>> GetHistoryShips(string id)
        {
            return await _context.Historys.Where(h => h.Plate == id).ToListAsync();
        }

        public async Task<IEnumerable<History>> Get()
        {
            return await _context.Historys.ToListAsync();
        }

        public async Task Create(History history, string userId)
        {
            var ship = await _context.Shipss.FindAsync(history.Plate);
            var airport = await _context.Airports.FindAsync(history.AirportID);

            if (ship is null || airport is null)
                throw new Exception();

            _context.Historys.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(History history, string userId)
        {
            var historyBD = await GetHistory(history);
            historyBD.OwnerRole = history.OwnerRole;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(History history, string userId)
        {
            var historyBD = await GetHistory(history);
            _context.Historys.Remove(historyBD);
            await _context.SaveChangesAsync();
        }

        public async Task Include(History history)
        {
            var historyDB = await GetHistory(history);
            historyDB.OwnerRole = history.OwnerRole;
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------
        private async Task<History> GetHistory(History history)
        {
            var historyBD = await _context.Historys.SingleOrDefaultAsync(h =>
            h.Plate == history.Plate && h.AirportID == history.AirportID && h.Date == history.Date);

            if (historyBD is null)
                throw new Exception();
            return historyBD;
        }
    }
}
