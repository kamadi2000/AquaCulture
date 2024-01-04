using Fish_Farm.DTOs;
using Fish_Farm.Entities;

namespace Fish_Farm.Services.ClientService
{
    public interface IClientService
    {
        Task<List<Client>> GetAll(HttpRequest request);
    }
}
