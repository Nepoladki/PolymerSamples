using PolymerSamples.Models;

namespace PolymerSamples.Interfaces
{
    public interface IUserRepository
    {
        ICollection<Users> GetAllUsers();
        Users GetUser(Guid id);
        bool UserNameExists(string userName);
        bool UserExists(Guid id);
        bool CreateUser(Users user);
        bool UpdateUser(Users user);
        bool DeleteUser(Users user);
        bool Save();

    }
}
