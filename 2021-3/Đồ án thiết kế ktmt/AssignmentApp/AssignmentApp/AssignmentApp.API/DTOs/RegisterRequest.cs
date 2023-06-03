using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics;

namespace AssignmentApp.API.DTOs;

public class RegisterRequest
{
    [Required]
    [StringLength(30,ErrorMessage = "Must be between 3 and 30 characters " , MinimumLength = 3)]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Confirm Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [StringLength(15, ErrorMessage = "Phone number must be at least  12 character")]
    public string PhoneNumber { get; set; }
    
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string FullName { get; set; }
    public string? MSSV { get; set; }
    
    [Required]
    public int RoleId { get; set; }
}