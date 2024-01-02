using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ClientController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetAlClients()
        {
            var clients = await _dataContext.ClientTable
                .Include(client => client.fishFarms)
                .ToListAsync();
            return Ok(clients);
            
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(Client client)
        {
            _dataContext.ClientTable.Add(client);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteClient(int id)
        {
            var client = await _dataContext.ClientTable.FindAsync(id);
            if (client == null)
            {
                return BadRequest("User not found");
            }
            _dataContext.ClientTable.Remove(client);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpPut]
        public async Task<ActionResult<Client>> EditClient(Client newClient)
        {
            var client = await _dataContext.ClientTable
                .Include(client => client.fishFarms)
                .FirstOrDefaultAsync(f => f.Id == newClient.Id);
            if (client == null)
            {
                return BadRequest("User not found");
            }

            return Ok(client);

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
                    fishFarms = x.fishFarms
                })
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(client);
            }
        }

        [HttpPost("{clientId}")]
        public async Task<ActionResult<string>> AddClientFishfarm(int clientId, ClientFishfarm clientFishfarm) 
        {
            var workerList = await _dataContext.WorkerTable
                               .Where(worker => clientFishfarm.WorkersIdList.Contains(worker.Id))
                               .ToListAsync();
            var fishFarm = await _dataContext.FishFarmTable.FindAsync(clientFishfarm.FishFarmId);
            var client = await _dataContext.ClientTable.FindAsync(clientId);

            if (fishFarm == null)
            {
                return BadRequest("Fish farm not found");
            }
            if (fishFarm.Workers == null)
            {
                fishFarm.Workers = workerList;
            }
            else
            {
                var newList = fishFarm.Workers.Concat(workerList).Distinct().ToList();
                fishFarm.Workers = newList;

            }
            
            if (client == null)
            {
                return BadRequest("Client not found");
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



        
    }
}
