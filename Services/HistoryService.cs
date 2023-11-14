using AIRCOM.Models;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class HistoryService
    {
        private readonly DBContext _context;
        public HistoryService(DBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<History>> GetHistoryShips(string id)
        {
            return await _context.Histories.Where(h => h.Plate == id).ToListAsync();
        }

        public async Task<IEnumerable<History>> Get()
        {
            return await _context.Histories.ToListAsync();
        }

        public async Task Create(History history)
        {
            var ship = await _context.Ships.FindAsync(history.Plate);
            var airport = await _context.Airports.FindAsync(history.AirportID);

            if (ship is null || airport is null)
                throw new Exception();

            _context.Histories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(History history)
        {
            var historyBD = await GetHistory(history);
            historyBD.OwnerRole = history.OwnerRole;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(History history)
        {
            var historyBD = await GetHistory(history);
            _context.Histories.Remove(historyBD);
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------
        private async Task<History> GetHistory(History history)
        {
            var historyBD = await _context.Histories.SingleOrDefaultAsync(h =>
            h.Plate == history.Plate && h.AirportID == history.AirportID && h.Date == history.Date);

            if (historyBD is null)
                throw new Exception();
            return historyBD;
        }
    }
}
