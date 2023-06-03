using AssignmentApp.Data.EF;
using Microsoft.EntityFrameworkCore;
using File = AssignmentApp.Data.Entities.File;

namespace AssignmentApp.API.Repository.Files;

public class FileRepository:IFileRepository
{
    private readonly AssignmentAppDbContext _context;
    public FileRepository(AssignmentAppDbContext context)
    {
        _context = context;
    }
    // lay file tu student assignment
    public async Task<List<File>> GetFileFromStudentAssignment(int assignmentId , int studentId)
    {
        var queryable = from f in _context.Files
            where f.StudentAssignment.AssignmentId == assignmentId & f.StudentAssignment.StudentId == studentId
            select f;
        var files = await  queryable.ToListAsync();
        return files;
    }
}