namespace TaskManagerApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }  // Task ID
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    public int UserId { get; set; }  // Foreign Key
}
