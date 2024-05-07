using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Models;

namespace PolymerSamples.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(Users user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(Users user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public ICollection<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Users GetUser(Guid id) => _context.Users.Where(u => u.Id == id).FirstOrDefault();

        public bool Save() => _context.SaveChanges() > 0;

        public bool UpdateUser(Users user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool UserExists(Guid id) => _context.Users.Any(u => u.Id == id);

        public bool UserNameExists(string userName) => _context.Users.Any(u => u.UserName == userName);
        
    }
}
