using Fish_Farm.DTOs;

namespace Fish_Farm.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAll();
        Task<bool> DeleteUser(int id);
    }
}
