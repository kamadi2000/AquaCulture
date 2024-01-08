using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Fish_Farm.Repositories.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;
        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<HttpStatusCode> AddClient(Client client)
        {
            _dataContext.ClientTable.Add(client);
            await _dataContext.SaveChangesAsync();
            return (HttpStatusCode.OK);
        }

        public async Task<HttpStatusCode> DeleteClient(int clientId)
        {
            var client = await _dataContext.ClientTable.FindAsync(clientId);
            if (client == null)
            {
                return (HttpStatusCode.BadRequest);
            }
            _dataContext.ClientTable.Remove(client);
            await _dataContext.SaveChangesAsync();
            return (HttpStatusCode.OK);
        }

        public async Task<HttpStatusCode> EditClient(Client newClient)
        {
            var client = await _dataContext.ClientTable
                .Include(client => client.fishFarms)
                .FirstOrDefaultAsync(f => f.Id == newClient.Id);
            if (client == null)
            {
                return (HttpStatusCode.BadRequest);
            }
            return (HttpStatusCode.OK);
        }

        public async Task<List<Client>> GetAll(HttpRequest request)
        {
            var clients = await _dataContext.ClientTable
                .Include(client => client.fishFarms)
                .ToListAsync();
            return clients;
        }
    }
}
