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

        public async Task<IEnumerable<HistoryDTO>> Get(string? id = null)
        {
            List<History> histories;
            if (id is null)
                histories = await _context.Historys.Include(h => h.Ships).ToListAsync();
            else
                histories = await _context.Historys.Include(h => h.Ships).Where(h => h.Plate == id).ToListAsync();
            return _mapper.Map<List<HistoryDTO>>(histories);
        }

        public async Task Create(HistoryDTO history, string userId)
        {
            if (history.OwnerRole == "Ninguno")
                history.OwnerRole = null;

            await Errors(history, userId);
            var arrival = _mapper.Map<History>(history);
            var exit = _mapper.Map<History>(history);

            arrival.ArrivalID = int.Parse(userId);
            arrival.ExitDate = null;
            arrival.ArrivalDate = DateTime.Now;

            exit.ExitID = int.Parse(userId);

            _context.Historys.Add(arrival);
            _context.Historys.Add(exit);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(HistoryDTO history, string userId)
        {
            await Errors(history, userId);
            var historyBD = await GetHistoryById(history.ID);
            historyBD.OwnerRole = history.OwnerRole;
            historyBD.Plate = history.Plate;
            historyBD.ArrivalDate = history.ArrivalDate;
            historyBD.ExitDate = history.ExitDate;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(HistoryDTO history, string userId)
        {
            await Errors(history, userId);
            var historyBD = await GetHistoryById(history.ID);
            _context.Historys.Remove(historyBD);
            await _context.SaveChangesAsync();
        }

        public async Task Include(HistoryDTO history)
        {
            var historyDB = await GetHistoryById(history.ID);
            historyDB.OwnerRole = history.OwnerRole;
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------
        private async Task<History> GetHistoryById(int? id)
        {
            var historyBD = await _context.Historys.SingleOrDefaultAsync(h => h.ID == id);
            if (historyBD is null)
                throw new Exception("Este vuelo no existe");
            return historyBD;
        }

        private async Task Errors(HistoryDTO history, string? userId = null)
        {
            var ship = await _context.Shipss.FindAsync(history.Plate);
            if (ship is null)
                throw new Exception("La nave no existe");

            var fly = await _context.Historys.SingleOrDefaultAsync(h => h.Plate == history.Plate && 
                ((h.ArrivalDate.Value.Year == DateTime.Now.Year && h.ArrivalDate.Value.DayOfYear == DateTime.Now.DayOfYear && h.ArrivalID == int.Parse(userId)) || 
                (h.ExitDate.Value.Year == history.ExitDate.Value.Year && h.ExitDate.Value.DayOfYear == history.ExitDate.Value.DayOfYear && h.ExitID == int.Parse(userId))));
            if (fly is not null)
                throw new Exception("Esta nave ya está en el aeropuerto");
            
            ship.State = history.State;
            if (ship.NextFly is null)
                ship.NextFly = history.ExitDate;
            else
                ship.NextFly = (ship.NextFly < history.ExitDate) ? ship.NextFly : history.ExitDate;
        }
    }
}
