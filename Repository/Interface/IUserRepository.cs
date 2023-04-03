using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.Repository.Interface
{
    public interface IUserRepository : IDisposable
    {
        Task<UserDto> GetById(int id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<string> AddNewUser(PostUserDto postuserDto, IFormFile file);
        Task UpdateUser(int id, UserDto userDto);
        Task DeleteUser(int id);
    }
        
}
