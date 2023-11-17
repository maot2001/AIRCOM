using AIRCOM.Models;
using AIRCOM.Models.DTO;
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

        public async Task<IEnumerable<HistoryDTO>> Get(string id = "")
        {
            List<History> histories;
            if (id == "")
                histories = await _context.Historys.ToListAsync();
            else
                histories = await _context.Historys.Where(h => h.Plate == id).ToListAsync();
            return _mapper.Map<List<HistoryDTO>>(histories);
        }

        public async Task Create(HistoryDTO history, string userId)
        {
            await Errors(history);
            var historyDB = _mapper.Map<History>(history);
            historyDB.AirportID = int.Parse(userId);
            historyDB.Ships = await _context.Shipss.FindAsync(history.Plate);
            historyDB.Airport = await _context.Airports.FindAsync(int.Parse(userId));
            _context.Historys.Add(historyDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(HistoryDTO history, string userId)
        {
            await Errors(history, userId);
            var historyBD = await GetHistoryById(history.Id);
            historyBD.OwnerRole = history.OwnerRole;
            historyBD.Plate = history.Plate;
            historyBD.Date = history.Date;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(HistoryDTO history, string userId)
        {
            await Errors(history, userId);
            var historyBD = await GetHistoryById(history.Id);
            _context.Historys.Remove(historyBD);
            await _context.SaveChangesAsync();
        }

        public async Task Include(HistoryDTO history)
        {
            var historyDB = await GetHistoryById(history.Id);
            historyDB.OwnerRole = history.OwnerRole;
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------
        private async Task<History> GetHistoryById(int id)
        {
            var historyBD = await _context.Historys.SingleOrDefaultAsync(h => h.Id == id);
            if (historyBD is null)
                throw new Exception();
            return historyBD;
        }

        private async Task Errors(HistoryDTO history, string userId = "")
        {
            if (userId != "" && history.AirportID != int.Parse(userId))
                throw new Exception();

            var ship = await _context.Shipss.FindAsync(history.Plate);
            if (ship is null)
                throw new Exception();

            var fly = await _context.Historys.SingleOrDefaultAsync(h => h.Plate == history.Plate && h.Date == history.Date);
            if (fly is not null)
                throw new Exception();
        }
    }
}
