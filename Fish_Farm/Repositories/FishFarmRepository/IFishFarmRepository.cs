using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using System.Net;
using System.Reflection.Metadata;

namespace Fish_Farm.Repositories.FishFarmRepository
{
    public interface IFishFarmRepository
    {
        Task<List<FishFarm>> GetAll(HttpRequest request);
        Task<bool> AddFishFarm(FishFarmDTO fishFarmDTO);
        Task<bool> DeleteFishfarm(int id);
        Task<FishFarm?> GetFishFarmById(HttpRequest request,int id);
        Task<bool> EditFishfarm(FishFarmDTO fishFarm);
        Task<bool> AddClientFishfarm(int clientId, FishFarm fihsfarm);
        Task<ICollection<Worker>> GetFishfarmWorkers(int fishfarmId);

    }
}
