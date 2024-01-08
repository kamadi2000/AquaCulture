using Azure;
using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.DTOs.ClientDTOs;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Security.Claims;

namespace Fish_Farm.Repositories.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        
        private readonly DataContext _dataContext;
        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Boolean> AddClient(AddClientDTO client)
        {
            var usr_db = (from u in _dataContext.UserTable
                          where u.Email == client.UserEmail
                          select u).FirstOrDefault();
            _dataContext.ClientTable.Add(new Client
            {
                Name = client.Name,
                UserId = usr_db.Id
            });
            await _dataContext.SaveChangesAsync();
            return (true);
        }

        public async Task<Boolean> DeleteClient(int clientId)
        {
            var client = await _dataContext.ClientTable.FindAsync(clientId);
            if (client == null)
            {
                return (false);
            }
            _dataContext.ClientTable.Remove(client);
            await _dataContext.SaveChangesAsync();
            return (true);
        }

        public async Task<Boolean> EditClient(Client newClient)
        {
            var client = await _dataContext.ClientTable
                .Include(client => client.fishFarms)
                .FirstOrDefaultAsync(f => f.Id == newClient.Id);
            if (client == null)
            {
                return (false);
            }
            return (true);
        }

        public async Task<bool> ManageClientFishfarm(int clientId, ClientFishfarm clientFishfarm)
        {
            var fishFarm = await _dataContext.FishFarmTable.FindAsync(clientFishfarm.FishFarmId);
            var client = await _dataContext.ClientTable.FindAsync(clientId);

            if (fishFarm == null)
            {
                return false;
            }
            var existingWorkers = await _dataContext.WorkerTable
                    .Where(worker => worker.FishFarmId == clientFishfarm.FishFarmId)
                    .ToListAsync();
            foreach (var worker in existingWorkers)
            {
                if (!clientFishfarm.WorkersIdList.Contains(worker.Id))
                {
                    worker.FishFarmId = null;
                }
            }
            await _dataContext.SaveChangesAsync();
            var newWorkerList = await _dataContext.WorkerTable
                               .Where(worker => clientFishfarm.WorkersIdList.Contains(worker.Id))
                               .ToListAsync();

            foreach (var worker in newWorkerList)
            {
                worker.FishFarmId = clientFishfarm.FishFarmId;
            }
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Client>> GetAll(HttpRequest request, String email)
        {
            var usr_db = (from u in _dataContext.UserTable
                          where u.Email == email
                          select u).FirstOrDefault();
            var clients = await _dataContext.ClientTable
                .Where(ct => ct.UserId == usr_db.Id)
                .Include(client => client.fishFarms)
                .ToListAsync();
            return clients;
        }

        public async Task<Client?> GetClientById(HttpRequest request,int id)
        {
            var client = await _dataContext.ClientTable
                .Include(c => c.fishFarms)
                .Select(x => new Client()
                {
                    Id = x.Id,
                    Name = x.Name,
                    fishFarms = x.fishFarms.Select(x => new FishFarm()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Latitude = x.Latitude,
                        Longitude = x.Longitude,
                        Num_of_cages = x.Num_of_cages,
                        Has_barge = x.Has_barge,
                        ImageName = x.ImageName,
                        Workers = x.Workers,
                        ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName),
                    }).ToList()
                })
            .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return null;
            }
            else
            {
                return client;
            }
        }
    }
}
