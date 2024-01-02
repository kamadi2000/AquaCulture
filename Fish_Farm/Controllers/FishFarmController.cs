using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Fish_Farm.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FishFarmController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FishFarmController(DataContext dataContext, IWebHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<FishFarm>>> GetAllFishFarms()
        {
            var fishfarms = await _dataContext.FishFarmTable
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
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                })
                .ToListAsync();
            return Ok(fishfarms);
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddFishFarm(FishFarm fishFarm)
        {
            if (fishFarm.ImageName != null)
            {
                fishFarm.ImageName = await SaveImage(fishFarm.ImageFile);
            }
            _dataContext.FishFarmTable.Add(fishFarm);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteFishFarm(int id)
        {
            var farm= await _dataContext.FishFarmTable.FindAsync(id);
            if (farm == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok("Successful");
            }
        }

        [HttpPut]
        public async Task<ActionResult<FishFarm>> EditFishFarm(FishFarm fishFarm)
        {
            var farm = await _dataContext.FishFarmTable.FindAsync(fishFarm.Id);
            if (farm == null)
            {
                return BadRequest("Not found");
            }
            if (fishFarm.ImageName != null)
            {
                farm.ImageName = await SaveImage(fishFarm.ImageFile);
            }
            farm.Latitude = fishFarm.Latitude;
            farm.Longitude = fishFarm.Longitude;
            farm.Num_of_cages = fishFarm.Num_of_cages;
            farm.Has_barge = fishFarm.Has_barge;
            farm.Name = fishFarm.Name;
            await _dataContext.SaveChangesAsync();
            return Ok(farm);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FishFarm>> FindFishFarmById(int id)
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
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName),
                })
                .FirstOrDefaultAsync(f => f.Id == id);
            if (farm == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok(farm);
            }
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<FishFarm>> FindFishFarmByClientId(int clientId)
        {
            var client = await _dataContext.ClientTable.FindAsync(clientId);
            
            if (client == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok(client.fishFarms);
            }
        }
        //[HttpGet("client/fishfarms")]
        //public async Task<ActionResult<List<FishFarm>>> FindNotOwnedFishFarms()
        //{
        //    var fishfarms = await _dataContext.FishFarmTable
        //            .Join()
        //}

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
