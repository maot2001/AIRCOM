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
            {
                var dto = _mapper.Map<ClientDTO>(client);
                dto.Rol = "Cliente";
                result.Add(dto);
            }

            foreach (var worker in workers)
            {
                var dto = _mapper.Map<ClientDTO>(worker);
                dto.Rol = "Trabajador";
                result.Add(dto);
            }
            return result;
        }

        public async Task Create(ClientDTO client, string userId)
        {
            await Errors(client);

            if (types.Contains(client.Type))
            {
                var worker = _mapper.Map<Worker>(client);
                if (worker.AirportID == 0)
                    worker.AirportID = int.Parse(userId);
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
                var worker = await GetWorkerById(client.Email);
                worker.Name = client.Name;
                worker.CI = client.CI;
                worker.Email = client.Email;
                worker.Nationality = client.Nationality;
                worker.Type = client.Type;
            }

            else
            {
                var clientDB = await GetClientById(client.Email);
                clientDB.Name = client.Name;
                clientDB.CI = client.CI;
                clientDB.Email = client.Email;
                clientDB.Nationality = client.Nationality;
                clientDB.Type = client.Type;
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string email)
        {
            try
            {
                var worker = await GetWorkerById(email);
                _context.Workers.Remove(worker);
            }
            catch
            {
                var clientDB = await GetClientById(email);
                if (clientDB.On_Sites.Count == 0)
                    await Waterfall(clientDB, true);
                else
                    await Waterfall(clientDB, false);
            }
            await _context.SaveChangesAsync();
        }

        //---------------------------------------------------------

        private async Task Waterfall(Client client, bool delete)
        {
            foreach (var ship in client.Shipss)
            {
                if (delete)
                {
                    if(ship.Reports.Count == 0)
                        _context.Shipss.Remove(ship);
                    else
                    {
                        ship.Active = false;
                        ship.ClientID = 0;
                    }
                }
                else
                    ship.Active = false;
            }

            if(delete)
                _context.Clients.Remove(client);
            else
                client.Active = false;
        }

        private async Task<Client> GetClientById(string? email)
        {
            var client = await _context.Clients
                .Include(c => c.On_Sites)
                .Include(c => c.Shipss).ThenInclude(s => s.Reports)
                .SingleOrDefaultAsync(c => c.Email == email);

            if (client is null)
                throw new Exception("El cliente no existe");

            return client;
        }

        private async Task<Worker> GetWorkerById(string? email)
        {
            var worker = await _context.Workers.SingleOrDefaultAsync(w => w.Email == email);

            if (worker is null)
                throw new Exception("El trabajador no existe");

            return worker;
        }

        private async Task Errors(ClientDTO client)
        {
            var errors = await _context.Clients.SingleOrDefaultAsync(c => (c.CI == client.CI && c.Nationality == client.Nationality) || c.Email == client.Email);
            var errors2 = await _context.Workers.SingleOrDefaultAsync(w => (w.CI == client.CI && w.Nationality == client.Nationality) || w.Email == client.Email);

            if (errors is not null || errors2 is not null)
                throw new Exception("Credenciales existentes");
        }
    }
}
