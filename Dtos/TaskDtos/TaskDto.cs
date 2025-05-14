using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Dtos;

public class TaskDto
{
    [Required]
    [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    [Required(ErrorMessage = "User ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive integer.")]
    public int UserId { get; set; }
}
