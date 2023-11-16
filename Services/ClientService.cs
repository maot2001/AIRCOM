using AIRCOM.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AIRCOM.Services
{
    public class ClientService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ClientService(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Client>> Get(string userId)
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task Create(Client client, string userId)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, Client client)
        {
            var clientBD = await GetClientById(id);
                
            clientBD.Name = client.Name;
            clientBD.Type = client.Type;
            clientBD.Nationality = client.Nationality;
            clientBD.Email = client.Email;
            clientBD.Pwd = client.Pwd;
            
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var client = await GetClientById(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        //---------------------------------------------------------
        private async Task<Client> GetClientById(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client is null)
                throw new Exception();
            return client;
        }
    }
}
