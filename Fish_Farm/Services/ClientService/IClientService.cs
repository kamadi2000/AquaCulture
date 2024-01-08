using Fish_Farm.DTOs;
using Fish_Farm.DTOs.ClientDTOs;
using Fish_Farm.Entities;
using System.Net;

namespace Fish_Farm.Services.ClientService
{
    public interface IClientService
    {
        Task<List<GetClientDTO>> GetAll(HttpRequest request, String email);
        Task<Boolean> AddClient(AddClientDTO client);
        Task<Boolean> DeleteClient(int clientId);
        Task<Boolean> EditClient(Client client);
        Task<Client?> GetClientById(HttpRequest request,int clientId);
        Task<Boolean> ManageClientFishfarm(int client, ClientFishfarm clientFishfarm);
    }
}
