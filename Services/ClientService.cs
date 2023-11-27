using AIRCOM.Models;
using AIRCOM.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ClientService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly string[] types = new string[] { "Seguridad", "Administrador", "Mecánico", "Dirección" };
        public ClientService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDTO>> Get(string userId)
        {
            var clients = await _context.Clients.ToListAsync();
            var workers = await _context.Workers.Where(w => w.AirportID == int.Parse(userId)).ToListAsync();
            List<ClientDTO> result = new();
            foreach (var client in clients)
                result.Add(_mapper.Map<ClientDTO>(client));
            foreach (var worker in workers)
                result.Add(_mapper.Map<ClientDTO>(worker));
            return result;
        }

        public async Task Create(ClientDTO client, string userId)
        {
            await Errors(client);

            if (types.Contains(client.Type))
            {
                var worker = _mapper.Map<Worker>(client);
                worker.AirportID = int.Parse(userId);
                var airport = await _context.Airports.SingleOrDefaultAsync(a => a.AirportID == worker.AirportID);
                worker.Airport = airport;
                _context.Workers.Add(worker);
            }

            else
            {
                var clientDB = _mapper.Map<Client>(client);
                _context.Clients.Add(clientDB);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Edit(ClientDTO client)
        {
            await Errors(client);

            if (types.Contains(client.Type))
            {
                var worker = await GetWorkerById(client.ClientID);
                worker.Name = client.Name;
                worker.CI = client.CI;
                worker.Email = client.Email;
                worker.Nationality = client.Nationality;
                worker.Type = client.Type;
            }

            else
            {
                var clientDB = await GetClientById(client.ClientID);
                clientDB.Name = client.Name;
                clientDB.CI = client.CI;
                clientDB.Email = client.Email;
                clientDB.Nationality = client.Nationality;
                clientDB.Type = client.Type;
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(ClientDTO client)
        {
            if (types.Contains(client.Type))
            {
                var worker = await GetWorkerById(client.ClientID);
                _context.Workers.Remove(worker);
            }

            else
            {
                var clientDB = await GetClientById(client.ClientID);
                _context.Clients.Remove(clientDB);
            }

            await _context.SaveChangesAsync();
        }

        //---------------------------------------------------------
        private async Task<Client> GetClientById(int? id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client is null)
                throw new Exception("El cliente no existe");

            return client;
        }

        private async Task<Worker> GetWorkerById(int? id)
        {
            var worker = await _context.Workers.FindAsync(id);

            if (worker is null)
                throw new Exception("El trabajador no existe");

            return worker;
        }

        private async Task Errors(ClientDTO client)
        {
            var errors = await _context.Clients.SingleOrDefaultAsync(c => c.CI == client.CI && c.Nationality == client.Nationality);

            if (errors is not null)
                throw new Exception("Credenciales existentes");
        }
    }
}
