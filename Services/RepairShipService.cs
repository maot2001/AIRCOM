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
            var rep = await _context.Repairs.FindAsync(repair.RepairID);
            var RS = _mapper.Map<RepairShip>(repair);
            RS.Plate = shipId;
            RS.State = ship.State;

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
                    repairs = await _context.RepairShips
                        .Where(SR => SR.Init == null && SR.RepairInstallation.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 1:
                    repairs = await _context.RepairShips
                        .Where(SR => SR.Finish == null && SR.Init != null && SR.RepairInstallation.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 2:
                    repairs = await _context.RepairShips
                        .Where(SR => SR.Finish != null && SR.RepairInstallation.Installation.AirportID == int.Parse(userId)).ToListAsync();
                    break;
                case 3:
                    repairs = await _context.RepairShips
                        .Where(SR => SR.RepairInstallation.Installation.AirportID == int.Parse(userId)).ToListAsync();
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
            r.RepairInstallation.ID == repair.RepairInstallationID && r.Plate == repair.Plate);
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

            var exist = await _context.RepairShips.SingleOrDefaultAsync(rs =>
            rs.RepairInstallation.InstallationID == repair.InstallationID && rs.RepairInstallation.RepairID == repair.RepairID &&
            rs.Plate == shipId && rs.Init == null);
            if (exist is not null)
                throw new Exception();

            repair.Name = repairDB.Name;
            repair.Price = repairDB.Price;
        }
    }
}
