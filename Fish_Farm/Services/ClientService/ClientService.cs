using Fish_Farm.DTOs;
using Fish_Farm.DTOs.ClientDTOs;
using Fish_Farm.Entities;
using Fish_Farm.Repositories.ClientRepository;
using System.Net;

namespace Fish_Farm.Services.ClientService
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<Boolean> AddClient(AddClientDTO client)
        {
            return await _clientRepository.AddClient(client);
        }

        public async Task<Boolean> DeleteClient(int clientId)
        {
            return await _clientRepository.DeleteClient(clientId);
        }

        public Task<Boolean> EditClient(Client client)
        {
            return _clientRepository.EditClient(client);
        }

        public Task<bool> ManageClientFishfarm(int clientId, ClientFishfarm clientFishfarm)
        {
            return _clientRepository.ManageClientFishfarm(clientId, clientFishfarm);
        }

        public async Task<List<GetClientDTO>> GetAll(HttpRequest request, String email)
        {
            var clients = await _clientRepository.GetAll(request,email);
            return clients.Select(x => new GetClientDTO()
            {
                Id = x.Id,
                Name = x.Name,
                ClientEmail = x.ClientEmail,
            })
            .ToList();
        }
        public async Task<Client?> GetClientById(HttpRequest request,int clientId)
        {
            return await _clientRepository.GetClientById(request,clientId);
        }
    }
}
