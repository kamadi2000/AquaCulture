using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Services.FishFarmService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fish_Farm.Controllers
{
    [Authorize(Roles = "ClientAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FishFarmController : ControllerBase
    {
        private readonly IFishFarmService _fishFarmService;

        public FishFarmController(IFishFarmService fishFarmService)
        {
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
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteFishFarm(int id)
        {
            var status = await _fishFarmService.DeleteFishfarm(id);
            if (status) 
            {
                return Ok("Deleted successfully.");
            }
            return BadRequest("Not deleted.");
        }

        [HttpPut]
        public async Task<ActionResult<string>> EditFishFarm(FishFarmDTO fishFarm)
        {
            var status = await _fishFarmService.EditFishfarm(fishFarm);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Not changed.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FishFarm>> FindFishFarmById(int id)
        {
            var fishfarm =  await _fishFarmService.GetFishFarmById(Request, id);
            if (fishfarm == null)
            {
                return BadRequest("Fishfarm not found");
            }
            return Ok(fishfarm);
        }

        //[HttpPost("{clientId}")]
        //public async Task<ActionResult<string>> AddClientFishfarm(int id, FishFarm fishFarm)
        //{
        //    var status = await _fishFarmService.AddClientFishfarm(id, fishFarm);
        //    if (status) { return Ok("Successful"); }
        //    return BadRequest("Client fish farm not added.");
        //}

        [HttpGet("clientFishFarms/{fishfarmId}")]
        public async Task<ActionResult<List<Worker>>> GetFishFarmWorkers(int fishfarmId)
        {
            var workers = await _fishFarmService.GetFishfarmWorkers(fishfarmId);
            if (workers == null)
            {
                return BadRequest("Fish farm not found");
            }
            return Ok(workers);
        }


    }
}
