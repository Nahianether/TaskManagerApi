namespace TaskManagerApi.Dtos;

public class UserUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
}
