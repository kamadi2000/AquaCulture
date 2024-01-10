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
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;

namespace Fish_Farm.Controllers
{
    [Authorize(Roles = "ClientAdmin")]
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
        [HttpGet("UserClient/{email}")]
        public async Task<ActionResult<List<GetClientDTO>>> GetAlClients(string email)
        {
            return Ok(await _clientService.GetAll(Request, email));

        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(AddClientDTO client)
        {
            var status = await _clientService.AddClient(client);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Client not added.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteClient(int id)
        {
            var status = await _clientService.DeleteClient(id);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Client not deleted.");
        }

        [HttpPut]
        public async Task<ActionResult<string>> EditClient(Client newClient)
        {
            var status = await _clientService.EditClient(newClient);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Client not edited or client not found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> FindClientById(int id)
        {
            var client = await _clientService.GetClientById(Request,id);
            if (client == null)
            {
                return BadRequest("Client not found.");
            }
            return Ok(client);
        }

        [HttpPut("{clientId}")]
        public async Task<ActionResult<string>> ManageClientFishfarm(int clientId, ClientFishfarm clientFishfarm)
        {
            var status = await _clientService.ManageClientFishfarm(clientId, clientFishfarm);
            if (status)
            {
                return Ok("Successful");
            }
            return BadRequest("Not changed.");
        }
    }
}
