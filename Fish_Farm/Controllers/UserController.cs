using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Fish_Farm.Entities;
using Fish_Farm.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public UserController(DataContext dataContext,IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return await _userService.GetAll();
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddUser(User user)
        {
            _dataContext.UserTable.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var user  = await _userService.DeleteUser(id);
            if (user)
            {
                return Ok("Successful");
            }
            return BadRequest("User not found");

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
