using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<List<TaskItem>> GetTasksByUserIdAsync(int userId);
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<bool> UpdateTaskAsync(int id, TaskItem updated);
    Task<bool> DeleteTaskAsync(int id);
}
