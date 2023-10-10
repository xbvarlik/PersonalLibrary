using System.ComponentModel.DataAnnotations;

namespace PersonalLibrary.Core.DTOs.AuthDTOs;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "Password is required")]
    public string NewPassword { get; set; } = null!;
    
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
    public string NewPasswordConfirm { get; set; } = null!;
}