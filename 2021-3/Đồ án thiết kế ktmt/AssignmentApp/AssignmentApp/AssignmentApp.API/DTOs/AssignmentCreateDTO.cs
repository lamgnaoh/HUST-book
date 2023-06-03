namespace AssignmentApp.API.DTOs;

public class AssignmentCreateDto
{
    public DateTime DueTo { get; set; }
    public DateTime CreateAt { get; set; }
    
    public string Title { get; set; }
    public string Content { get; set; }
}