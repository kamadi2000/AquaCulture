using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
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
            return Ok(await _dataContext.ClientTable.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(Client client)
        {
            _dataContext.ClientTable.Add(client);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteClient(int id)
        {
            var client = await _dataContext.ClientTable.FindAsync(id);
            if (client == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok("Successful");
            }
        }

        [HttpPut]
        public async Task<ActionResult<User>> EditClient(Client newClient)
        {
            var client = await _dataContext.ClientTable.FindAsync(newClient.Id);
            if (client == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(newClient);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> FindClientById(int id)
        {
            var client = await _dataContext.ClientTable.FindAsync(id);
            if (client == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(client);
            }
        }
    }
}
