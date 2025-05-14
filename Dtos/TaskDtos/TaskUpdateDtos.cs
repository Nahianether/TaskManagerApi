namespace TaskManagerApi.Dtos;

public class TaskUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
