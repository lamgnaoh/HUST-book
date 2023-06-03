using Microsoft.AspNetCore.Identity;

namespace AssignmentApp.Data.Entities;

public class AppRole
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    
    //navigation properties
    
    public IEnumerable<UserRole> UserRoles { get; set; }
}