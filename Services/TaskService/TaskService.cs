using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;
    private readonly Random _rnd = new();

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync() =>
        await _context.Tasks.ToListAsync();

    public async Task<TaskItem?> GetTaskByIdAsync(int id) =>
        await _context.Tasks.FindAsync(id);

    public async Task<List<TaskItem>> GetTasksByUserIdAsync(int userId) =>
        await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == task.UserId))
            throw new Exception("User does not exist.");

        int id;
        do
        {
            id = _rnd.Next(1000, 10000);
        } while (await _context.Tasks.AnyAsync(t => t.Id == id));

        task.Id = id;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskAsync(int id, TaskItem updated)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        task.Title = updated.Title;
        task.IsCompleted = updated.IsCompleted;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
