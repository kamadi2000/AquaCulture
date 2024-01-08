using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Repositories.FishFarmRepository;
using System.Net;

namespace Fish_Farm.Services.FishFarmService
{
    public class FishFarmService : IFishFarmService
    {
        private readonly IFishFarmRepository _repository;
        public FishFarmService(IFishFarmRepository repository) 
        {
            _repository = repository;
        }
        public async Task<List<FishFarmDTO>> GetAll(HttpRequest request)
        {
            var fishFarms = await _repository.GetAll(request);
            return fishFarms.Select(x => new FishFarmDTO
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Has_barge = x.Has_barge,
                ImageFile = x.ImageFile,
                ImageSrc = x.ImageSrc,
                ImageName = x.Name,
                Num_of_cages = x.Num_of_cages,
                ClientId = x.ClientId,
            }).ToList();     
        }
        public async Task<bool> AddFishFarm(FishFarmDTO fishFarmDTO)
        {
            return await _repository.AddFishFarm(fishFarmDTO);
        }

        public async Task<bool> DeleteFishfarm(int id)
        {
            return await _repository.DeleteFishfarm(id);
        }

        public async Task<FishFarm?> GetFishFarmById(HttpRequest request,int id)
        {
            return await _repository.GetFishFarmById(request, id);
        }

        public async Task<bool> EditFishfarm(FishFarm fishFarm)
        {
            return await _repository.EditFishfarm(fishFarm);
        }

        public async Task<bool> AddClientFishfarm(int clientId, FishFarm fishfarm)
        {
            return await _repository.AddClientFishfarm(clientId, fishfarm);
        }

        public async Task<List<Worker>?> GetFishfarmWorkers(int fishfarmId)
        {
            return await _repository.GetFishfarmWorkers(fishfarmId);
        }
    }
}
