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
        public async Task<HttpStatusCode> AddFishFarm(FishFarmDTO fishFarmDTO)
        {
            if (fishFarmDTO.ImageName != null)
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
            return (HttpStatusCode.OK);
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
