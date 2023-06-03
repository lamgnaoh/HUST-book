using System.ComponentModel.DataAnnotations;
using AssignmentApp.Data.Entities;

namespace AssignmentApp.API.DTOs;

public class UserCreateRequestDto
{
    [Required]
    [StringLength(30,ErrorMessage = "Must be between 3 and 30 characters " , MinimumLength = 3)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }
    [Required]
    public string FullName { get; set; }
    public string? MSSV { get; set; }
    [Required]
    public List<int> RoleIDs { get; set; }
}