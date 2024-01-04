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
        public async Task<HttpStatusCode> AddFishFarm(FishFarmDTO fishFarmDTO)
        {
            return await _repository.AddFishFarm(fishFarmDTO);
        }

    }
}
