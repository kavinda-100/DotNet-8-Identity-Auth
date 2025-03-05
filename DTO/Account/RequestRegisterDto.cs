using System.ComponentModel.DataAnnotations;

namespace DotNet_8_Identity_Auth.DTO.Account;

public class RequestRegisterDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Username must be at most 50 characters long.")]
    public String FullName { get; set; } = String.Empty;
    
    [Required]
    [EmailAddress]
    public String Email { get; set; } = String.Empty;
    
    [Required]
    [MinLength(6, ErrorMessage = "password must be at least 6 characters long.")]
    [MaxLength(12, ErrorMessage = "password must be at most 12 characters long.")]
    public String Password { get; set; } = String.Empty;
}