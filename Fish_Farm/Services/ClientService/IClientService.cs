using Fish_Farm.DTOs;
using Fish_Farm.DTOs.ClientDTOs;
using Fish_Farm.Entities;
using System.Net;

namespace Fish_Farm.Services.ClientService
{
    public interface IClientService
    {
        Task<List<GetClientDTO>> GetAll(HttpRequest request);
        Task<HttpStatusCode> AddClient(Client client);
        Task<HttpStatusCode> DeleteClient(int clientId);
        Task<HttpStatusCode> EditClient(Client client);
    }
}
