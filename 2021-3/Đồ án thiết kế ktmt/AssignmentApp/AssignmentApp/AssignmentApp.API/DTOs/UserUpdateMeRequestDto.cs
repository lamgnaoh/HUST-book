using System.ComponentModel.DataAnnotations;

namespace AssignmentApp.API.DTOs;

public class UserUpdateMeRequestDto
{
    
    [Required]
    [StringLength(255, ErrorMessage = "Must be between 8  characters", MinimumLength = 8)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 8  characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}