using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(int id, User updatedUser);
    Task<bool> DeleteUserAsync(int id);
}
