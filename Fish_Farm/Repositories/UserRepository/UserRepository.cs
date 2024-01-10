using Fish_Farm.Data;
using Fish_Farm.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        { 
            _dataContext = dataContext;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _dataContext.UserTable.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _dataContext.UserTable.Remove(user);    
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _dataContext.UserTable
                .Select(x => new UserDTO
                {
                    Name = x.Name,
                    Email = x.Email,
                    Id = x.Id,
                })
                .ToListAsync();
            return (users);
        }
    }
}
