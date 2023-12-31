﻿using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class RepairInstallationService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public RepairInstallationService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RepairInstallationDTO>> Get(string? userId = null, int? id = null)
        {
            List<RepairInstallation> repairsDB = new();
            if (userId is not null)
            {
                repairsDB = await _context.RepairInstallations
                    .Include(ri => ri.Installation)
                    .Where(ri => ri.Installation.AirportID == int.Parse(userId)).ToListAsync();
            }
            if (id is not null)
            {
                repairsDB = await _context.RepairInstallations
                    .Include(ri => ri.Installation)
                    .Where(ri => ri.InstallationID == id).ToListAsync();
            }
            var repairs = _mapper.Map<List<RepairInstallationDTO>>(repairsDB);
            foreach (var repair in repairs)
            {
                var installation = await _context.Installations.FindAsync(repair.InstallationID);
                repair.InstallationID = installation.InstallationID;
            }
            return repairs;
        }

        public async Task<RepairInstallationDTO> GetComments(string? userId, int? id)
        {
            var inst = await _context.RepairInstallations.Include(ri => ri.RepairShips).SingleOrDefaultAsync(ri => ri.ID == id);
            var result = _mapper.Map<RepairInstallationDTO>(inst);
            return result;
        }

        public async Task Create(RepairInstallationDTO repair)
        {
            await Errors(repair);

            var repairInstallation = _mapper.Map<RepairInstallation>(repair);

            _context.RepairInstallations.Add(repairInstallation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(RepairInstallationDTO repair)
        {
            await Errors(repair);

            var repairDB = await GetRepairInstallation(repair);
            repairDB.Price = repair.Price;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(RepairInstallationDTO repair)
        {
            var repairDB = await GetRepairInstallation(repair);

            if (repairDB.RepairShips.Count == 0)
                _context.RepairInstallations.Remove(repairDB);
            else
                repairDB.Active = false;

            await _context.SaveChangesAsync();
        }

        //-------------------------------------------------------------------------
        public async Task<RepairInstallation> GetRepairInstallation(RepairInstallationDTO repair)
        {
            var installationDB = await _context.Installations.FindAsync(repair.InstallationID);
            
            var repairDB = await _context.RepairInstallations
                .Include(ri => ri.Installation)
                .Include(ri => ri.RepairShips)
                .SingleOrDefaultAsync(ri => ri.InstallationID == installationDB.ID && ri.Installation.AirportID == installationDB.AirportID && ri.RepairID == repair.RepairID);

            if (repairDB is null)
                throw new Exception("En esta instalación no se realiza esta reparación");

            return repairDB;
        }

        private async Task Errors(RepairInstallationDTO repair)
        {
            var installationDB = await _context.Installations.FindAsync(repair.InstallationID);

            var repairDB = await _context.Repairs.FindAsync(repair.RepairID);
            if (repairDB is null)
                throw new Exception("La reparación no existe");

            var exist = await _context.RepairInstallations
                .Include(ri => ri.Installation)
                .SingleOrDefaultAsync(ri => ri.InstallationID == installationDB.ID && (ri.RepairID == repair.RepairID || ri.Name == repair.Name));
            if (exist is not null)
                throw new Exception("En esta instalación ya se realiza esta reparación");

            repair.Name = repairDB.Name;
        }
    }
}
