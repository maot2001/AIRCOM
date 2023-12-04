using Microsoft.VisualBasic.CompilerServices;
using System.Security.Authentication;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Collections.Generic;
using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace AIRCOM.Services
{
    public class ConsultService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ConsultService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //1
        public async Task<List<AirportDTO>> GetPoint1()
        {
            var airports = await _context.Airports
                .Include(a => a.Installations).ThenInclude(i => i.RepairInstallations)
                .ToListAsync();
            List<Airport> exit = new List<Airport>();
            
            foreach (var airport in airports)
            {
                foreach (var installation in airport.Installations)
                {
                    if(installation.RepairInstallations.Count != 0)
                    {
                        exit.Add(airport);
                        continue;
                    }
                }
            }
            
            return _mapper.Map<List<AirportDTO>>(exit);
        }
        //2
        //cantidad de reparaciones capitales en cada aeropuerto
        public async Task<Dictionary<AirportDTO, int>> GetPoint2()
        {
            var airports = await _context.Airports.ToListAsync();
            var repairShips = await _context.RepairShips.ToListAsync();
            //List<(Airport, int)> exit = new List<(Airport, int)>();

            Dictionary<Airport, int> exit = new Dictionary<Airport, int>();

            foreach (var airport in airports)
            {
                //exit.Add(airport, 0);
                int count = 0;

                foreach (var installation in airport.Installations)
                {
                    foreach (var repairInstallation in installation.RepairInstallations)
                    {
                        foreach (var repairShip in repairShips)
                        {
                            if(repairShip.Name == "Capital") count++;
                        }
                    }
                }
                exit.Add(airport, count);
            }

            return _mapper.Map<Dictionary<AirportDTO, int>>(exit);
        }
       
        
        public async Task<List<ClientDTO>> GetPoint3()
        {

            var historys = await _context.Historys.ToListAsync();
            List<Client> exit = new List<Client>();

            foreach (var history in historys)
            {
                if (history.ArrivalAirport!= null &&  history.ArrivalAirport.Name == "José Martí")
                {

                    if ((history.OwnerRole == "Capitán")) exit.Add(history.Ships.Clients);
                }

            }
            return _mapper.Map<List<ClientDTO>>(exit);

        }
        //4 
      
        public async Task<List<(Airport,int)>> GetPoint4()
        {
            DateTime inicio = new DateTime(2010, 1, 1);
            Dictionary<Airport,int> exit = new Dictionary<Airport,int>();
            List<(Airport, int)> exit1 = new List<(Airport, int)>();
            var historys = await _context.Historys.ToListAsync();
            int count = 0;


            foreach(var history in historys)
            {
                if (history.ArrivalDate != null && history.ArrivalDate > inicio)
                {
                    if(exit.ContainsKey(history.ArrivalAirport)) exit[history.ArrivalAirport]++;
                    
                    else exit.Add(history.ArrivalAirport,1);
                }
                var a = exit.OrderBy(v => v.Value);
                
                
            }
            foreach(var item in exit)
            {
                if (count > 4) break;
                count++;

                int aux = 0;

                foreach(var RepairInstallation in item.Key.Installations)
                {
                    aux++;
                }
                exit1.Add((item.Key, aux));


            }
            return exit1;
            
        }
        //
    }
}
        public async Task<List<(RepairInstallation, float)>> GetPoint5()
        {

            var airports = await _context.Airports.ToListAsync();
            List<(RepairInstallation, float)> exit = new List<(RepairInstallation, float)>();

            foreach (var airport in airports)
            {
                if (airport.Name == "Jose Martí")
                {

                    foreach (var installation in airport.Installations)
                    {
                        foreach (var repairInstallation in installation.RepairInstallations)
                        {
                            
                        float sum = 0;
                        int count = 0;
                        bool itHappened = false;

                        foreach (var repairShip in repairInstallation.RepairShips)
                        {
                            sum += repairShip.Price;
                            count++;
                            if (!repairShip.Eficient) itHappened = true;
                        }
                        if (itHappened) exit.Add((repairInstallation, sum / count));
                        }

                    }
                }

            }
            return _mapper.Map<List<(RepairInstallation, float)>>(exit);
        }