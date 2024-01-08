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

        public async Task<HttpStatusCode> AddClient(Client client)
        {
            return await _clientRepository.AddClient(client);
        }

        public async Task<HttpStatusCode> DeleteClient(int clientId)
        {
            return await _clientRepository.DeleteClient(clientId);
        }

        public Task<HttpStatusCode> EditClient(Client client)
        {
            return _clientRepository.EditClient(client);
        }

        public async Task<List<GetClientDTO>> GetAll(HttpRequest request)
        {
            var clients = await _clientRepository.GetAll(request);
            return clients.Select(x => new GetClientDTO()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .ToList();
        }
    }
}
