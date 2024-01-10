using Fish_Farm.DTOs;

namespace Fish_Farm.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<bool> DeleteUser(int id);
    }
}
