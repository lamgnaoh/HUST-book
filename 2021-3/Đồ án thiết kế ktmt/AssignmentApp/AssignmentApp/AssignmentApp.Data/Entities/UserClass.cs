
namespace AssignmentApp.Data.Entities;

public class UserClass
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int ClassId { get; set; }
    public Class Class { get; set; }
}