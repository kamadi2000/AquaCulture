using Fish_Farm.Data;
using Fish_Farm.Entities;
using Fish_Farm.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Fish_Farm.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DataContext _datacontext;
        public static User user = new User();
        private IConfiguration _config;
        Dictionary<string, string> credentials = new Dictionary<string, string>();
        public AuthController(DataContext dataContext, IConfiguration config)
        {
            _datacontext = dataContext;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User_DTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Name = request.Name;
            user.Birthday = request.Birthday;
            user.Image = request.Image;
            _datacontext.UserTable.Add(user);
            await _datacontext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginCredentials credentials)
            //email - kmaliyanage@gmail.com , password - home
        {
            TokenGenerator tokenGenerator = new TokenGenerator(_config);

            var usr_db  = (from u in _datacontext.UserTable
                        where u.Email == credentials.Email
                        select u).FirstOrDefault();
            
            if ( usr_db == null)
            {
                return BadRequest("Invalid Credentials");
            }
            else if (VerifyPasswordHash(credentials.Password, usr_db.PasswordHash, usr_db.PasswordSalt)) 
            {
                var token = tokenGenerator.GenerateToken(usr_db.Name, credentials.Email); 
                return Ok(token);
            }
            return BadRequest("Invalid Credentials");
            
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string Password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

    }
}
