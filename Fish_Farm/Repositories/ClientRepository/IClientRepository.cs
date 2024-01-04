using Fish_Farm.Entities;

namespace Fish_Farm.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll(HttpRequest request);
    }
}
