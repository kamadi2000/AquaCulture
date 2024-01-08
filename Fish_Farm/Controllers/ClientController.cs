using Azure.Core;
using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.DTOs.ClientDTOs;
using Fish_Farm.Entities;
using Fish_Farm.Services.ClientService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IClientService _clientService;

        public ClientController(DataContext dataContext, IClientService clientService)
        {
            _dataContext = dataContext;
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetClientDTO>>> GetAlClients()
        {
            return Ok(await _clientService.GetAll(Request));
            
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(Client client)
        {
            var status = await _clientService.AddClient(client);
            if (status == System.Net.HttpStatusCode.OK)
            {
                return Ok("Successful");
            }
            return BadRequest(status);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteClient(int id)
        {
            var status = await _clientService.DeleteClient(id);
            if (status == System.Net.HttpStatusCode.OK)
            {
                return Ok("Successful");
            }
            return BadRequest(status);
        }

        [HttpPut]
        public async Task<ActionResult<string>> EditClient(Client newClient)
        {
            var status = await _clientService.EditClient(newClient);
            if (status == System.Net.HttpStatusCode.OK)
            {
                return Ok("Successful");
            }
            return BadRequest(status);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> FindClientById(int id)
        {
            var client = await _dataContext.ClientTable
                .Include(c => c.fishFarms)
                .Select(x => new Client()
                {
                    Id = x.Id,
                    Name = x.Name,
                    fishFarms = x.fishFarms.Select(x => new FishFarm()
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
                    }).ToList()
                })
            .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return BadRequest("Client not found");
            }
            else
            {
                return client;
            }
        }

        [HttpPut("{clientId}")]
        public async Task<ActionResult<string>> AddClientFishfarm(int clientId, ClientFishfarm clientFishfarm) 
        {
            
            var fishFarm = await _dataContext.FishFarmTable.FindAsync(clientFishfarm.FishFarmId);
            var client = await _dataContext.ClientTable.FindAsync(clientId);

            if (fishFarm == null)
            {
                return BadRequest("Fish farm not found");
            }
            var existingWorkers = await _dataContext.WorkerTable
                    .Where(worker => worker.FishFarmId == clientFishfarm.FishFarmId)
                    .ToListAsync();
            foreach (var worker in existingWorkers)
            {
                if (!clientFishfarm.WorkersIdList.Contains(worker.Id))
                {
                    worker.FishFarmId = null;
                }
            }
            await _dataContext.SaveChangesAsync();
            var newWorkerList = await _dataContext.WorkerTable
                               .Where(worker => clientFishfarm.WorkersIdList.Contains(worker.Id))
                               .ToListAsync();

            foreach (var worker in newWorkerList)
            {
                worker.FishFarmId = clientFishfarm.FishFarmId;
            }
            await _dataContext.SaveChangesAsync();           
            return Ok("Successful");
        }



        
    }
}
