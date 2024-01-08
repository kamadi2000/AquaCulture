using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Fish_Farm.Repositories.FishFarmRepository
{
    public class FishFarmRepository : IFishFarmRepository
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FishFarmRepository(DataContext dataContext, IWebHostEnvironment hostEnvironment) 
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;

        }

        public async Task<List<FishFarm>> GetAll(HttpRequest request)
        {
            return await _dataContext.FishFarmTable
               .Include(w => w.Workers)
               .Select(x => new FishFarm()
               {
                   Id = x.Id,
                   Name = x.Name,
                   Latitude = x.Latitude,
                   Longitude = x.Longitude,
                   Num_of_cages = x.Num_of_cages,
                   Has_barge = x.Has_barge,
                   ImageName = x.ImageName,
                   Workers = x.Workers,
                   ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName),
               })
               .ToListAsync();
        }
        public async Task<Boolean> AddFishFarm(FishFarmDTO fishFarmDTO)
        {
            if (fishFarmDTO.ImageFile != null)
            {
                fishFarmDTO.ImageName = await SaveImage(fishFarmDTO.ImageFile);
            }
            _dataContext.FishFarmTable.Add(new FishFarm
            {
                Name = fishFarmDTO.Name,
                Latitude = fishFarmDTO.Latitude,
                Longitude = fishFarmDTO.Longitude,
                ClientId = fishFarmDTO.ClientId,
                Num_of_cages = fishFarmDTO.Num_of_cages,
                Has_barge = fishFarmDTO.Has_barge,
                ImageName = fishFarmDTO.ImageName,
                ImageFile = fishFarmDTO.ImageFile,
            });

            await _dataContext.SaveChangesAsync();
            return (true);
        }
        public async Task<bool> DeleteFishfarm(int id)
        {
            var currentWorkers = await _dataContext.WorkerTable
                .Where(wt => wt.FishFarmId == id)
                .ToListAsync();
            foreach (var worker in currentWorkers)
            {
                worker.FishFarmId = null;
            }
            await _dataContext.SaveChangesAsync();
            var farm = await _dataContext.FishFarmTable.FindAsync(id);
            if (farm == null)
            {
                return false;
            }
            _dataContext.FishFarmTable.Remove(farm);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        public async Task<FishFarm?> GetFishFarmById(HttpRequest request, int id)
        {
            var farm = await _dataContext.FishFarmTable
                .Include(f => f.Workers)
                .Select(x => new FishFarm()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Num_of_cages = x.Num_of_cages,
                    Has_barge = x.Has_barge,
                    ImageName = x.ImageName,
                    Workers = x.Workers,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, x.ImageName),
                })
                .FirstOrDefaultAsync(f => f.Id == id);
            if (farm == null)
            {
                return null;
            }
            else
            {
                return (farm);
            }
        }

        public async Task<bool> EditFishfarm(FishFarm fishFarm)
        {
            var farm = await _dataContext.FishFarmTable.FindAsync(fishFarm.Id);
            if (farm == null)
            {
                return false;
            }
            if (fishFarm.ImageFile != null)
            {
                farm.ImageName = await SaveImage(fishFarm.ImageFile);
            }
            farm.Latitude = fishFarm.Latitude;
            farm.Longitude = fishFarm.Longitude;
            farm.Num_of_cages = fishFarm.Num_of_cages;
            farm.Has_barge = fishFarm.Has_barge;
            farm.Name = fishFarm.Name;
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddClientFishfarm(int clientId, FishFarm fishFarm)
        {
            if (fishFarm.ImageFile != null)
            {
                fishFarm.ImageName = await SaveImage(fishFarm.ImageFile);
            }
            _dataContext.FishFarmTable.Add(fishFarm);
            var client = await _dataContext.ClientTable.FindAsync(clientId);
            if (client == null)
            {
                return false;
            }
            if (client.fishFarms == null)
            {
                List<FishFarm> fishFarmList = new List<FishFarm>();
                fishFarmList.Add(fishFarm);
                client.fishFarms = fishFarmList;
                await _dataContext.SaveChangesAsync();
                return true;

            }
            client.fishFarms.Add(fishFarm);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Worker>?> GetFishfarmWorkers(int fishfarmId)
        {
            var fishfarm = await _dataContext.FishFarmTable
                .Include(ft => ft.Workers)
                .FirstOrDefaultAsync(ft => ft.Id == fishfarmId);
            if (fishfarm == null)
            {
                return null;
            }
            var fishfarmWorkers = fishfarm.Workers;
            if (fishfarmWorkers == null)
            {
                return new List<Worker>();
            }
            return (List<Worker>)fishfarmWorkers;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            Console.WriteLine(imageName);
            return imageName;
        }
    }
}
