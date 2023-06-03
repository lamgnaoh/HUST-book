namespace AssignmentApp.Data.Entities;

public class File
{
    public int FileId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
//navigation property
    public StudentAssignment StudentAssignment { get; set; }
}