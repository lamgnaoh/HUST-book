using Microsoft.AspNetCore.Identity;

namespace AssignmentApp.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string MSSV { get; set; }
    public string FullName { get; set; }


  // navigation property
    public ICollection<UserClass> UserClasses { get; set; }
    public ICollection<StudentAssignment> StudentAssignments { get; set; }
    
    public List<UserRole> UserRoles { get; set; }
    // public List<Class> Classes { get; set; }

}