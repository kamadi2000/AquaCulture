using Fish_Farm.Data;
using Fish_Farm.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _dataContext.UserTable.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(User user)
        {
            _dataContext.UserTable.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var user  = await _dataContext.UserTable.FindAsync(id);
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
        public async Task<ActionResult<User>> EditUser(User newUser)
        {
            var user = await _dataContext.UserTable.FindAsync(newUser.Id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(newUser);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> FindUserById(int id)
        {
            var user =  await _dataContext.UserTable.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found");
            }
            else
            {
                return Ok(user);
            }
        }
    }
}
