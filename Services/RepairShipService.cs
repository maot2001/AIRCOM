using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class RepairShipService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly RepairInstallationService _aux;
        public RepairShipService(DBContext context, IMapper mapper, RepairInstallationService aux)
        {
            _context = context;
            _mapper = mapper;
            _aux = aux;
        }

        public async Task RequestRepair(string shipId, RepairInstallationDTO repair)
        {
            await Errors(repair, shipId);

            var ship = await _context.Shipss.FindAsync(shipId);
            var RS = _mapper.Map<RepairShip>(repair);
            RS.ID = 0;
            RS.State = ship.State;
            RS.Name = repair.Name;

            _context.RepairShips.Add(RS);
            await _context.SaveChangesAsync();
        }

        public async Task ProcessRepair(RepairShipDTO repair)
        {
            var shipRepair = await RepairAux(repair);
            shipRepair.Init = (DateTime)repair.newTime;
            await _context.SaveChangesAsync();
        }

        public async Task FinishRepair(RepairShipDTO repair)
        {
            var shipRepair = await RepairAux(repair);
            
            TimeSpan? time = (DateTime) repair.newTime - shipRepair.Init;
            int cost = shipRepair.Time - (int) time?.TotalHours;
            if (cost > 0)
                shipRepair.Price *= (1 + cost / 100);

            shipRepair.Finish = (DateTime)repair.newTime;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepairShipDTO>> Get(int type, string? userId = null, int id = 0)
        {
            List<RepairShip> repairs;
            switch (type)
            {
                case 0:
                    repairs = await _context.RepairShips.Where(SR => SR.Init == default(DateTime) && SR.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 1:
                    repairs = await _context.RepairShips.Where(SR => SR.Finish == default(DateTime) && SR.Init != default(DateTime) && SR.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 2:
                    repairs = await _context.RepairShips.Where(SR => SR.Finish != default(DateTime) && SR.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 3:
                    repairs = await _context.RepairShips.Where(SR => SR.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                default:
                    repairs = await _context.RepairShips.Where(SR => SR.Ships.ClientID == id).ToListAsync();
                    break;
            }

            return _mapper.Map<List<RepairShipDTO>>(repairs);
        }

        public async Task Valorate(RepairShipDTO repair)
        {
            var repairShipDB = await RepairAux(repair);
            repairShipDB.Stars = repair.Stars;
            repairShipDB.Comment = repair.Comment;

            var repairDB = await _aux.GetRepairInstallation(_mapper.Map<RepairInstallationDTO>(repair));
            var prom = repairDB.Stars * (float) repairDB.Votes;
            prom += repairShipDB.Stars;
            repairDB.Votes++;
            prom /= repairDB.Votes;
            repairDB.Stars = prom;

            await _context.SaveChangesAsync();
        }

        // ---------------------------------------------------------------------
        private async Task<RepairShip> RepairAux(RepairShipDTO repair)
        {
            if (repair.newTime == default(DateTime))
                repair.newTime = DateTime.Now;

            var shipRepair = await _context.RepairShips.SingleOrDefaultAsync(r =>
            r.InstallationID == repair.InstallationID && r.RepairID == repair.RepairID &&
            r.Plate == repair.Plate && r.Init == repair.Init);

            if (shipRepair is null)
                throw new Exception();
            return shipRepair;
        }

        private async Task Errors(RepairInstallationDTO repair, string shipId)
        {
            var repairDB = await _aux.GetRepairInstallation(repair);

            var ship = await _context.Shipss.FindAsync(shipId);
            if (ship is null)
                throw new Exception();

            var exist = _context.RepairShips.SingleOrDefaultAsync(rs =>
            rs.InstallationID == repair.InstallationID && rs.RepairID == repair.RepairID && rs.Plate == shipId);
            if (exist is not null)
                throw new Exception();
        }
    }
}
