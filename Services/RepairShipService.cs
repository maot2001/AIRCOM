using AIRCOM.Models;
using AIRCOM.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class RepairShipService
    {
        private readonly DBContext _context;
        public RepairShipService(DBContext context)
        {
            _context = context;
        }

        public async Task RequestRepair(string shipId, RepairInstallationDTO repairId)
        {
            var ship = await _context.Ships.FindAsync(shipId);
            var repair = await _context.RepairInstallations.SingleOrDefaultAsync(r =>
            r.InstallationID == repairId.InstallationID && r.AirportID == repairId.AirportID && r.RepairID == repairId.RepairID);

            if (ship is null || repair is null)
                throw new Exception();

            var RS = new RepairShip
            {
                Plate = shipId,
                RepairID = repair.RepairID,
                InstallationID = repair.InstallationID,
                AirportID = repair.AirportID,
                Ships = ship,
                Repair = repair.Repair,
                Installation = repair.Installation,
                Airport = repair.Airport,
                State = repairId.State,
                Price = repair.Price
            };

            _context.RepairShips.Add(RS);
            await _context.SaveChangesAsync();
        }

        public async Task ProcessRepair(RepairShipDTO repairId)
        {
            var shipRepair = await RepairAux(repairId);
            shipRepair.Init = (DateTime)repairId.newTime;
            await _context.SaveChangesAsync();
        }

        public async Task FinishRepair(RepairShipDTO repairId)
        {
            var shipRepair = await RepairAux(repairId);
            
            TimeSpan time = (DateTime)repairId.newTime - shipRepair.Init;
            int cost = shipRepair.Time - (int)time.TotalHours;
            if (cost > 0)
                shipRepair.Price *= (1 + cost / 100);

            shipRepair.Finish = (DateTime)repairId.newTime;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepairShip>> Get(int type)
        {
            List<RepairShip> repairs;
            switch (type)
            {
                case 0:
                    repairs = await _context.RepairShips.Where(SR => SR.Init == default(DateTime)).ToListAsync();
                    break;
                case 1:
                    repairs = await _context.RepairShips.Where(SR => SR.Finish == default(DateTime) && SR.Init != default(DateTime)).ToListAsync();
                    break;
                case 2:
                    repairs = await _context.RepairShips.Where(SR => SR.Finish != default(DateTime)).ToListAsync();
                    break;
                default:
                    repairs = await _context.RepairShips.ToListAsync();
                    break;
            }

            return repairs;
        }

        // ---------------------------------------------------------------------
        private async Task<RepairShip> RepairAux(RepairShipDTO repairId)
        {
            if (repairId.newTime == default(DateTime))
                repairId.newTime = DateTime.Now;

            var shipRepair = await _context.RepairShips.SingleOrDefaultAsync(r =>
            r.InstallationID == repairId.InstallationID && r.AirportID == repairId.AirportID && r.RepairID == repairId.RepairID &&
            r.Plate == repairId.Plate && r.Init == repairId.Init);

            if (shipRepair is null)
                throw new Exception();
            return shipRepair;
        }

    }
}
