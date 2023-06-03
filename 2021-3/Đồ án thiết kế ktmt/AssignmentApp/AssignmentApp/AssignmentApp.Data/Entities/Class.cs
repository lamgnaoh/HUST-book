namespace AssignmentApp.Data.Entities;

public class Class
{
    public int ClassId { get; set; }
    // public int UserCreateId { get; set; }
    public DateTime CreateAt { get; set; }
    public string Name { get; set; }
    
  // navigation property
    public ICollection<Assignment> Assignments { get; set; }
    public ICollection<UserClass> UserClasses { get; set; }
   // public User UserCreate { get; set; }
}