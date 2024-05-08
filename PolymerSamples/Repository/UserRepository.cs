using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> CreateUserAsync(Users user)
        {
            _context.Users.Add(user);
            return await SaveAsync();
        }

        public async Task<bool> DeleteUserAsync(Users user)
        {
            _context.Users.Remove(user);
            return await SaveAsync();
        }

        public async Task<ICollection<Users>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<Users?> GetUserByNameAsync(string name)
        {
            return await _context.Users.Where(u => u.UserName.Trim() == name.Trim()).FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserByIdAsync(Guid id) => await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;

        public async Task<bool> UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            return await SaveAsync();
        }

        public async Task<bool> UserExistsAsync(Guid id) => await _context.Users.AnyAsync(u => u.Id == id);

        public async Task<bool> UserNameExistsAsync(string userName) => await _context.Users.AnyAsync(u => u.UserName == userName);
        
    }
}
