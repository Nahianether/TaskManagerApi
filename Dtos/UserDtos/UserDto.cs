using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Dtos;

public class UserDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = string.Empty;
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }
    [Phone(ErrorMessage = "Invalid phone number.")]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
    public string? PhoneNumber { get; set; }
    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
    public string? Address { get; set; }
    [Url(ErrorMessage = "Invalid URL format.")]
    [RegularExpression(@"^(http|https)://.*$", ErrorMessage = "URL must start with http:// or https://")]
    public string? ImageUrl { get; set; }
}
