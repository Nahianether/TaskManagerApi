namespace TaskManagerApi.Dtos;

public class TaskCreateDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
}
