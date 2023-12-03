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
            var airports = await _context.Airports.ToListAsync();
            List<Airport> exit = new List<Airport>();

            foreach (var airport in airports)
            {
                if (airport.RepairInstallation.Count() != 0) exit.Add(airport);
            }

            return _mapper.Map<List<AirportDTO>>(exit);
        }
        //2
        public async Task<List<(AirportDTO, int)>> GetPoint2()
        {
            var airports = await _context.Airports.ToListAsync();
            List<(Airport, int)> exit = new List<(Airport, int)>();

            foreach (var airport in airports)
            {
                int CapitalReparationsCounter = 0;

                foreach (var item in airport.RepairShip)
                {
                    if (item.Name == "Reparación Capital") CapitalReparationsCounter++;
                }
                exit.Add((airport, CapitalReparationsCounter));
            }

            return _mapper.Map<List<(AirportDTO, int)>>(exit);
        }
       
        
        public async Task<List<ClientDTO>> GetPoint3()
        {

            var historys = await _context.Historys.ToListAsync();
            List<Client> exit = new List<Client>();

            foreach (var history in historys)
            {
                if (history.ArrivalAirport.Name == "José Martí")
                {

                    if ((history.OwnerRole == "Capitán")) exit.Add(history.Ships.Clients);
                }

            }
            return _mapper.Map<List<ClientDTO>>(exit);

        }
        //4 
      
        public async Task<List<(Airport,int)>> GetPoint4()
        {

            var date1 = new DateTime(2010, 1, 1);
            var date2 = new DateTime(2024, 1, 1);
            var change = new TimeSpan(366, 0, 0, 0);
            var airports = await _context.Airports.ToListAsync();
            var historys = await _context.Historys.ToListAsync();
            List<(Airport, int)> exit = new List<(Airport, int)>(); // pendiente ver si devuelvo un datetime o un entero para el anno

            while (date1 < date2)
            {
                Dictionary<Airport, int> aux = new Dictionary<Airport, int>();

                foreach (var history in historys)
                {
                    if (history.ArrivalDate != null && history.ArrivalDate > date1 &&
                    history.ArrivalDate < date2)
                    {
                        if (aux.ContainsKey(history.ArrivalAirport))
                            aux[history.ArrivalAirport] += 1;
                        else aux.Add(history.ArrivalAirport, 1);

                    }
                }
                Airport airport1 = airports[0];   // inicializo airport1 en cualquiera y despues lo actualizo
                int min = int.MaxValue;
                foreach (var airport in aux)
                {
                    if (airport.Value < min)
                    {
                        min = airport.Value;
                        airport1 = airport.Key;
                    }
                }
                exit.Add((airport1, date1.Year));
                date1 += change;
            }
            return _mapper.Map<List<(Airport, int)>>(exit);
        }
    }
}
