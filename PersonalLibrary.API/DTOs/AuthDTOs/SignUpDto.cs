using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.API.DTOs.AuthDTOs;

public class SignUpDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; } = null!;
    
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; } = null!;
    
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; } = null!;
}