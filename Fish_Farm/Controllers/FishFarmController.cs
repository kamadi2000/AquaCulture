using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FishFarmController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public FishFarmController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<FishFarm>>> GetAllFishFarms()
        {
            return Ok(await _dataContext.FishFarmTable.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddFishFarm(FishFarm fishFarm)
        {
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
            else
            {
                return Ok(fishFarm);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FishFarm>> FindFishFarmById(int id)
        {
            var farm = await _dataContext.FishFarmTable.FindAsync(id);
            if (farm == null)
            {
                return BadRequest("Not found");
            }
            else
            {
                return Ok(farm);
            }
        }
    }
}
