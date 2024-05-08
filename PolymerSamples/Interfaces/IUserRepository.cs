using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<Users>> GetAllUsersAsync();
        Task<Users> GetUserByIdAsync(Guid id);
        Task<Users?> GetUserByNameAsync(string name);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> UserExistsAsync(Guid id);
        Task<bool> CreateUserAsync(Users user);
        Task<bool> UpdateUserAsync(Users user);
        Task<bool> DeleteUserAsync(Users user);
        Task<bool> SaveAsync();

    }
}
