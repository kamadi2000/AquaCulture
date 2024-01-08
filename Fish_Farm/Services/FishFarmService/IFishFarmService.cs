using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using System.Net;

namespace Fish_Farm.Services.FishFarmService
{
    public interface IFishFarmService
    {
        Task<List<FishFarmDTO>> GetAll(HttpRequest request);
        Task<bool> AddFishFarm(FishFarmDTO fishFarmDTO);
        Task<bool> DeleteFishfarm(int id);
        Task<FishFarm?> GetFishFarmById(HttpRequest request,int id);
        Task<bool> EditFishfarm(FishFarm fishFarm);
        Task<bool> AddClientFishfarm(int clientId, FishFarm fihsfarm);
        Task<List<Worker>?> GetFishfarmWorkers(int fishfarmId);
    }
}
