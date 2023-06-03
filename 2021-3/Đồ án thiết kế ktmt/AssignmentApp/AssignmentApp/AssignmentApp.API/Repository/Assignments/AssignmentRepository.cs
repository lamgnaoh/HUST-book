using AssignmentApp.API.Utilities.Exception;
using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;


namespace AssignmentApp.API.Repository.Assignments;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly AssignmentAppDbContext _context;
    public AssignmentRepository(AssignmentAppDbContext context)
    {
        _context = context;
    }

    public async Task<Assignment> CreateAssignment(Assignment createAssignment)
    {
        var assignment = new Assignment()
        {
            ClassId = createAssignment.ClassId,
            CreateAt = createAssignment.CreateAt,
            DueTo = createAssignment.DueTo,
            Title = createAssignment.Title,
            Content = createAssignment.Content,
        };
        var query = from uc in _context.UserClasses
            where uc.ClassId == createAssignment.ClassId
            join u in _context.Users on uc.UserId equals u.Id
            join ur in _context.UserRoles on u.Id equals ur.UserId
             where ur.RoleId == 3
            select u.Id;
       
        await _context.Assignments.AddAsync(assignment);
        _context.SaveChanges();
        foreach (var studentId in query)
        {
            var studentAssignment = new Data.Entities.StudentAssignment()
            {
                AssignmentId = assignment.AssignmentId,
                StudentId = studentId,
                Submitted = false,
                Grade = null,
                Feedback = null,
                SubmittedAt = null
            };
            await _context.StudentAssignments.AddAsync(studentAssignment);
        }
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<Assignment> UpdateAssignment(Assignment updateAssignment , int id)
    {
        var existingAssignment = await _context.Assignments.FindAsync(id);
        if (existingAssignment == null)
        {
            throw new CustomException($"This assignment id is not found : {id}");
            return null;
        }

        existingAssignment.ClassId = updateAssignment.ClassId;
        existingAssignment.CreateAt = updateAssignment.CreateAt;
        existingAssignment.DueTo = updateAssignment.DueTo;
        existingAssignment.Content = updateAssignment.Content;
        existingAssignment.Title = updateAssignment.Title;
        await _context.SaveChangesAsync();
        return existingAssignment;
    }

    public async Task<Assignment> DeleteAssignment(int assignmentId)
    {
        var assignment = await _context.Assignments.FindAsync(assignmentId);
        var studentAssignment = await _context.StudentAssignments.FirstOrDefaultAsync(x=> x.AssignmentId == assignmentId);
        if (assignment == null)
        {
            throw new CustomException($"This assignment id is not found : {assignmentId}");
        }
        
        if (studentAssignment == null)
        {
            throw new CustomException($"This student  assignment id is not found : {assignmentId}");
        }
        
        _context.Assignments.Remove(assignment);
        _context.StudentAssignments.Remove(studentAssignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<List<Assignment>>  GetAllByClass(int classId)
    {
        var assigment = await _context.Assignments.Where(x => x.ClassId == classId).ToListAsync();
        return assigment;
    }
    
    public async Task<List<Assignment>>  GetAll()
    {
        return await _context.Assignments.ToListAsync();
    }

    public async Task<Assignment>  GetAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null)
        {
            throw new CustomException($"This assignment id is not found : {id}");
        }

        return assignment;
    }
    public async Task<Data.Entities.StudentAssignment> SubmitAssignment(int AssignmentId , int studentId,List<IFormFile> files)
    {
        var studentAssignment =
            await _context.StudentAssignments.FindAsync(AssignmentId,studentId);
        if (studentAssignment == null)
        {
            return null;
        }
        foreach (var item in files)
        {
            var file = new Data.Entities.File()
            {
                Name = item.FileName.Trim(),
                Path =   "AssignmentApp.API/Static/UploadFiles/"+ Path.GetFileName(item.FileName.Trim()),
                StudentAssignment = studentAssignment
            };
            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync();

        }
        //chinh sua student assignment 
        studentAssignment.Submitted = true;
        studentAssignment.SubmittedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return studentAssignment;
    }
}