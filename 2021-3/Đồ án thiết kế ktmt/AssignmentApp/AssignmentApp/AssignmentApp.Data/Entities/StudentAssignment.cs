namespace AssignmentApp.Data.Entities;

public class StudentAssignment
{
    public int AssignmentId { get; set; }
    public int StudentId { get; set; }
    public bool Submitted { set; get; }
    public double? Grade { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Feedback { get; set; }
    // navigation property
    public Assignment Assignment { get; set; }
    public User User { get; set; }
    public List<File> SubmitFiles { get; set; }
    // public User Student { get; set; }
}