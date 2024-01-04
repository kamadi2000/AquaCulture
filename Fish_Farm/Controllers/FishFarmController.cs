using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Services.FishFarmService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Fish_Farm.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FishFarmController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IFishFarmService _fishFarmService;

        public FishFarmController(DataContext dataContext, IWebHostEnvironment hostEnvironment, IFishFarmService fishFarmService)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
            _fishFarmService = fishFarmService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FishFarmDTO>>> GetAllFishFarms()
        {
            
            return Ok(await _fishFarmService.GetAll(Request));
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddFishFarm(FishFarmDTO fishFarmDTO)
        {
            var status = await _fishFarmService.AddFishFarm(fishFarmDTO);
            if (status == System.Net.HttpStatusCode.OK)
            {
                return Ok("Successful");
            }
            return BadRequest(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteFishFarm(int id)
        {
            var currentWorkers = await _dataContext.WorkerTable
                .Where(wt => wt.FishFarmId == id)
                .ToListAsync();
            foreach (var worker in currentWorkers)
            {
                worker.FishFarmId = null;
            }
            await _dataContext.SaveChangesAsync();
            var farm= await _dataContext.FishFarmTable.FindAsync(id);
            if (farm == null)
            {
                return BadRequest("Not found");
            }
            _dataContext.FishFarmTable.Remove(farm);
            await _dataContext.SaveChangesAsync();
            
            return Ok("Successful");
        }

        [HttpPut]
        public async Task<ActionResult<FishFarm>> EditFishFarm(FishFarm fishFarm)
        {
            var farm = await _dataContext.FishFarmTable.FindAsync(fishFarm.Id);
            if (farm == null)
            {
                return BadRequest("Not found");
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

        [HttpPost("{clientId}")]
        public async Task<ActionResult<string>> AddClientFishfarm(int id, FishFarm fishFarm)
        {
            if (fishFarm.ImageName != null)
            {
                fishFarm.ImageName = await SaveImage(fishFarm.ImageFile);
            }
            _dataContext.FishFarmTable.Add(fishFarm);
            var client = await _dataContext.ClientTable.FindAsync(id);
            if (client == null)
            {
                return BadRequest("User not found.");
            }
            if (client.fishFarms == null)
            {
                List<FishFarm> fishFarmList = new List<FishFarm>();
                fishFarmList.Add(fishFarm);
                client.fishFarms = fishFarmList;
                await _dataContext.SaveChangesAsync();
                return Ok("Successful");

            }
            client.fishFarms.Add(fishFarm);
            await _dataContext.SaveChangesAsync();

            return Ok("Successful");
        }
        [HttpGet("clientFishFarms/{fishfarmId}")]
        public async Task<ActionResult<List<Worker>>> GetFishFarmWorkers(int fishfarmId)
        {
            var fishfarm = await _dataContext.FishFarmTable
                .Include(ft => ft.Workers)
                .FirstOrDefaultAsync(ft => ft.Id == fishfarmId);
            if (fishfarm == null)
            {
                return BadRequest("Not found");
            }
            return Ok(fishfarm.Workers);
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
