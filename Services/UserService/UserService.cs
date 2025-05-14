using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly Random _rnd = new();

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync() =>
        await _context.Users.Include(u => u.Tasks).ToListAsync();

    public async Task<User?> GetUserByIdAsync(int id) =>
        await _context.Users.Include(u => u.Tasks).FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> CreateUserAsync(User user)
    {
        int id;
        do
        {
            id = _rnd.Next(1000, 10000);
        } while (await _context.Users.AnyAsync(u => u.Id == id));

        user.Id = id;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateUserAsync(int id, User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.Address = updatedUser.Address;
        user.ImageUrl = updatedUser.ImageUrl;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.Include(u => u.Tasks).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;

        _context.Tasks.RemoveRange(user.Tasks);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
