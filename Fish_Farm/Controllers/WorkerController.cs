using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fish_Farm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WorkerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddWorker(Worker worker)
        {
            _dataContext.WorkerTable.Add(worker);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteWorker(int id)
        {
            var user = await _dataContext.WorkerTable.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok("Successful");
            }
        }
        [HttpPut]
        public async Task<ActionResult<Worker>> EditWorker(Worker worker)
        {
            var user = await _dataContext.WorkerTable.FindAsync(worker.Id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(worker);
            }
        }

    }
}
