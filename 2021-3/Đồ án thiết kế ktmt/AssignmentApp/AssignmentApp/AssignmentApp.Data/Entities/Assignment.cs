namespace AssignmentApp.Data.Entities;

public class Assignment
{
    public int AssignmentId { get; set; }
    public int ClassId { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime DueTo { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
    //navigation property
    public Class Class { get; set; }
    public ICollection<StudentAssignment> StudentAssignments { get; set; }

}