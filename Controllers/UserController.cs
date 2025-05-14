using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.Include(u => u.Tasks).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.Include(u => u.Tasks)
                                       .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();
        return user;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        int id;
        var rnd = new Random();
        do
        {
            id = rnd.Next(1000, 10000); // generate 4-digit number
        }
        while (await _context.Users.AnyAsync(u => u.Id == id));

        user.Id = id;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User updatedUser)
    {
        if (id != updatedUser.Id)
            return BadRequest("User ID mismatch.");

        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null) return NotFound();

        // Update only allowed fields
        existingUser.Name = updatedUser.Name;
        existingUser.Email = updatedUser.Email;
        existingUser.PhoneNumber = updatedUser.PhoneNumber;
        existingUser.Address = updatedUser.Address;
        existingUser.ImageUrl = updatedUser.ImageUrl;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users
                                 .Include(u => u.Tasks)
                                 .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return NotFound();

        // Remove all tasks assigned to this user first (EF cascade should handle this if set)
        _context.Tasks.RemoveRange(user.Tasks);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
