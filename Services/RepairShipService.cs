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

        public async Task RequestRepair(string shipId, RepairInstallationDTO repair)
        {
            await Errors(repair, shipId);

            var ship = await _context.Shipss.FindAsync(shipId);

            var RS = _mapper.Map<RepairShip>(repair);
            RS.RepairInstallationID = repair.ID;
            RS.Plate = shipId;
            RS.State = ship.State;

            _context.RepairShips.Add(RS);
            await _context.SaveChangesAsync();

            await Dependencys(RS);
        }

        public async Task ProcessRepair(RepairShipDTO repair)
        {
            var shipRepair = await _context.RepairShips.FindAsync(repair.RSID);
            shipRepair.Init = DateTime.Now;
            var ship = await _context.Shipss.FindAsync(repair.Plate);

            if (shipRepair.Finish.Value.Year > ship.NextFly.Value.Year)
                shipRepair.Eficient = false;
            if (shipRepair.Finish.Value.Year == ship.NextFly.Value.Year && shipRepair.Finish.Value.DayOfYear > ship.NextFly.Value.DayOfYear)
                shipRepair.Eficient = false;

            await _context.SaveChangesAsync();
        }

        public async Task FinishRepair(RepairShipDTO repair)
        {
            var shipRepair = await _context.RepairShips
                .Include(rs => rs.Ships).ThenInclude(s => s.Reports)
                .SingleOrDefaultAsync(rs => rs.RSID == repair.RSID);
            shipRepair.Finish = DateTime.Now;
            
            TimeSpan? time = shipRepair.Finish - shipRepair.Init;
            float cost = shipRepair.Price * (float) time?.TotalHours;

            float val = 1;
            if (shipRepair.Ships.Reports.Count == 1)
                val += 0.01f * (float)time?.TotalHours;
            if (shipRepair.Ships.NextFly > shipRepair.Init && shipRepair.Ships.NextFly < shipRepair.Finish)
            {
                TimeSpan? time2 = shipRepair.Finish - shipRepair.Ships.NextFly;
                val -= 0.01f * (float)(time2?.TotalHours);
            }
            else if (shipRepair.Ships.NextFly < shipRepair.Init)
                shipRepair.Ships.NextFly = null;

            shipRepair.Price = cost * val;
            await _context.SaveChangesAsync();
        }


        public async Task Valorate(RepairShipDTO repair)
        {
            var repairShipDB = await _context.RepairShips.FindAsync(repair.RSID);
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
        private async Task Errors(RepairInstallationDTO repair, string shipId)
        {
            var repairDB = await _aux.GetRepairInstallation(repair);

            var ship = await _context.Shipss.FindAsync(shipId);
            if (ship is null)
                throw new Exception();

            var exist = await _context.RepairShips
                .SingleOrDefaultAsync(rs => rs.RepairInstallationID == repairDB.ID && rs.Plate == shipId && (rs.Finish == null || 
            (rs.Finish.Value.Year > DateTime.Now.Year && rs.Finish.Value.DayOfYear > DateTime.Now.DayOfYear)));
            if (exist is not null)
                throw new Exception();

            repair.ID = repairDB.ID;
            repair.Name = repairDB.Name;
            repair.Price = repairDB.Price;
        }

        private async Task Dependencys(RepairShip repair)
        {
            //Obtener dependencias de raparaciones
            var repairDB = await _context.RepairShips
                .Include(rs => rs.RepairInstallation)
                .SingleOrDefaultAsync(rs => rs.RSID == repair.RSID);
            var depends = await _context.Depends
                .Where(d => d.PrimaryID == repairDB.RepairInstallation.RepairID && d.State == repairDB.State).ToListAsync();

            foreach (var depend in depends)
            {
                //Revision de existencia de solciitud
                var newRS = await _context.RepairShips
                    .Include(rs => rs.RepairInstallation)
                    .SingleOrDefaultAsync(rs => rs.RepairInstallation.RepairID == depend.SecondID && rs.Plate == repair.Plate && rs.Finish > DateTime.Now);
                if (newRS is not null)
                    continue;

                //Revision de posibilidad de solicitud
                var repInst = await _context.RepairInstallations
                    .SingleOrDefaultAsync(ri => ri.RepairID == depend.SecondID && ri.InstallationID == repairDB.RepairInstallation.InstallationID);
                if (repInst is null)
                    continue;

                var rep = await _context.Repairs.FindAsync(depend.SecondID);
                newRS = new RepairShip()
                {
                    Name = rep.Name,
                    Plate = repair.Plate,
                    State = depend.State,
                    Price = repInst.Price,
                    RepairInstallationID = repInst.ID
                };

                _context.RepairShips.Add(newRS);
                await _context.SaveChangesAsync();
                Dependencys(newRS);
            }
        }
    }
}
