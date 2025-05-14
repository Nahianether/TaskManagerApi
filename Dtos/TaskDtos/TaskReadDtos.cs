namespace TaskManagerApi.Dtos;

public class TaskReadDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
