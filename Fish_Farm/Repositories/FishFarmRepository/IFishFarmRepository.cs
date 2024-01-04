using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using System.Net;
using System.Reflection.Metadata;

namespace Fish_Farm.Repositories.FishFarmRepository
{
    public interface IFishFarmRepository
    {
        Task<List<FishFarm>> GetAll(HttpRequest request);
        Task<HttpStatusCode> AddFishFarm(FishFarmDTO fishFarmDTO);
    }
}
