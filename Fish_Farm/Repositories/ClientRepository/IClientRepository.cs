using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using System.Net;

namespace Fish_Farm.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll(HttpRequest request);
        Task<HttpStatusCode> AddClient(Client client);
        Task<HttpStatusCode> DeleteClient(int clientId);
        Task<HttpStatusCode> EditClient(Client client);
        //Task<HttpStatusCode> GetClientById(int clientId);
    }
}
