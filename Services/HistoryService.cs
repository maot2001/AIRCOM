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
            await Errors(history, userId);
            var historyDB = _mapper.Map<History>(history);
            historyDB.Ships = await _context.Shipss.FindAsync(history.Plate);
            if (history.ArrivalID != 0)
                historyDB.ArrivalAirport = await _context.Airports.FindAsync(history.ArrivalID);
            if (history.ExitID != 0)
                historyDB.ExitAirport = await _context.Airports.FindAsync(history.ExitID);
            _context.Historys.Add(historyDB);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(HistoryDTO history, string userId)
        {
            await Errors(history, userId);
            var historyBD = await GetHistoryById(history.Id);
            historyBD.OwnerRole = history.OwnerRole;
            historyBD.Plate = history.Plate;
            historyBD.ArrivalDate = history.ArrivalDate;
            historyBD.ExitDate = history.ExitDate;
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
            if (userId != "" && ((history.ArrivalID != int.Parse(userId) && history.ExitID != int.Parse(userId)) 
                             ||  (history.ArrivalID == int.Parse(userId) && history.ExitID == int.Parse(userId))))
                throw new Exception();

            var ship = await _context.Shipss.FindAsync(history.Plate);
            if (ship is null)
                throw new Exception();

            var fly = await _context.Historys.SingleOrDefaultAsync(h => h.Plate == history.Plate && 
            ((h.ArrivalDate == history.ArrivalDate) || (h.ExitDate == history.ExitDate)));
            if (fly is not null)
                throw new Exception();
        }
    }
}
