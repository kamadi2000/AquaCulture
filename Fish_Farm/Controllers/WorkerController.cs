using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Services.WorkerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    [Authorize(Roles = "ClientAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<List<Worker>>> GetWorker(int clientId)
        {
            return await _workerService.GetAllWorkers(clientId, Request);
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddWorker([FromForm]WorkerDTO worker)
        {
            var status =  await _workerService.AddWorker(worker);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Worker not added.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteWorker(int id)
        {
            var status = await _workerService.DeleteWorker(id);
            if (status) 
            {
                return Ok("Successful");
            }
            return BadRequest("Worker not deleted.");
        }

        [HttpPut]
        public async Task<ActionResult<Worker>> EditWorker(Worker worker)
        {
            var status = await _workerService.EditWorker(worker);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Worker not deleted.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorkerById(int id)
        {
            var worker = await _workerService.GetWorkerById(id, Request);
            if (worker  == null)
            {
                return BadRequest("Worker not found.");
            }
            return Ok(worker);
        }
        [HttpGet("workers/{clientId}")]
        public async Task<ActionResult<List<Worker>>> GetUnEmployedWorkers(int clientId)
        {
            return await _workerService.GetUnEmployedWorkers(clientId);
        }

        

    }
}
