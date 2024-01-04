using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using System.Net;

namespace Fish_Farm.Services.FishFarmService
{
    public interface IFishFarmService
    {
        Task<List<FishFarmDTO>> GetAll(HttpRequest request);
        Task<HttpStatusCode> AddFishFarm(FishFarmDTO fishFarmDTO);
    }
}
