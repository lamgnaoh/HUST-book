using AssignmentApp.API.Utilities.Exception;
using AssignmentApp.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.API.Repository.StudentAssignment;

public class StudentAssignmentRepository : IStudentAssignmentRepository

{
    private readonly AssignmentAppDbContext _context;

    public StudentAssignmentRepository(AssignmentAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Data.Entities.StudentAssignment>> GetAllAssignedAssignment(int studentId)
    {
        
        var studentAssignment = await  _context.StudentAssignments.Where(x => x.StudentId == studentId).ToListAsync();
        return studentAssignment;
    }

    public async Task<List<Data.Entities.StudentAssignment>> GetAllStudentAssignmentForAssignment(int AssignmentId)
    {
        var studentAssignment =
            await _context.StudentAssignments.Where(x => x.AssignmentId == AssignmentId).ToListAsync();
        return studentAssignment;
    }

    public async Task<Data.Entities.StudentAssignment> GetStudentAssignment(int AssignmentId, int studentId)
    {
        return await _context.StudentAssignments.FindAsync(AssignmentId, studentId);
    }


    public async Task<Data.Entities.StudentAssignment> GradeAssignment(Data.Entities.StudentAssignment markedStudentAssignment,int AssignmentId, int StudentId)
    {
        var studentAssignment = await _context.StudentAssignments.FindAsync(AssignmentId, StudentId);
        if (studentAssignment == null)
        {
            return null;
        }
        
        // if (studentAssignment.Submitted == false)
        // {
        //     return null;
        //     
        // }

        studentAssignment.Grade = markedStudentAssignment.Grade;
        studentAssignment.Feedback = markedStudentAssignment.Feedback;
        await _context.SaveChangesAsync();
        return studentAssignment;
    }
}