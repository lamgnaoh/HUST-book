namespace AssignmentApp.API.DTOs;

public class AssignmentDto
{
    public int  Id { get; set; }
    public DateTime DueTo { get; set; }
    public DateTime CreateAt { get; set; }
    public int ClassID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    
}