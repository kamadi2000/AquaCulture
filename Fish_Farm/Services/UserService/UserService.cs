using Fish_Farm.DTOs;
using Fish_Farm.Repositories.UserRepository;

namespace Fish_Farm.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            return await _userRepository.GetAll();
        }
    }
}
