using AssignmentApp.API.Utilities.Exception;
using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace AssignmentApp.API.Repository.Classes;

public class ClassRepository : IClassRepository
{
    private readonly AssignmentAppDbContext _context;

    public ClassRepository(AssignmentAppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Class>> GetAll()
    {
        return await _context.Classes.ToListAsync();
    }

    public async Task<Class> CreateClass(Class createClass)
    {
        var newClass = new Class()
        {
            Name = createClass.Name,
            CreateAt = DateTime.Now
        };
        await _context.Classes.AddAsync(newClass);
        await _context.SaveChangesAsync();
        return newClass;
    }

    public async Task<Class> UpdateClass(Class updateClass , int classId)
    {
        var existingClass = await _context.Classes.FindAsync(classId);
        if (existingClass == null)
        {
            throw new CustomException($"Cannot find class with the id : {classId}");
            return null;
        }
        existingClass.Name = updateClass.Name;
        await _context.SaveChangesAsync();
        return existingClass;
    }

    public async Task<Class> DeleteClass(int classId)
    {
        var existingClass = await _context.Classes.FindAsync(classId);
        if (existingClass == null)
        {
            throw new CustomException($"This class  is not found  with id :  {classId}");
            return null;
        }

        var userClasses =await  _context.UserClasses.Where(x => x.ClassId == classId).ToListAsync();
        var assignments = await _context.Assignments.Where(x => x.ClassId == classId).ToListAsync();
        _context.UserClasses.RemoveRange(userClasses);
        _context.Assignments.RemoveRange(assignments);
        _context.Classes.Remove(existingClass);
        await _context.SaveChangesAsync();
        return existingClass;
    }

    public async  Task<Class> GetClass(int classId)
    {
        return await _context.Classes.FindAsync(classId);
    }
    
    // lấy tất cả các lớp học sinh tham dự
    public async Task<List<Class>> GetALlAttended(int userId)
    {
        var queryable = from uc in _context.UserClasses
                        join c in _context.Classes on uc.ClassId equals c.ClassId
                        join u in _context.Users on uc.UserId equals u.Id
                        where uc.UserId == userId 
                        select c;
        var classAttends = await queryable.ToListAsync();
        // var queryable2 = from uc in _context.UserClasses
        //     where uc.UserId == studentId
        //     select uc.ClassId;
        //
        // var test = new List<Class>();
        // foreach (var classId in queryable2 )
        // {
        //     var classAttend = await _context.Classes.FindAsync(classId);
        //     test.Add(classAttend);
        // }
        return classAttends;
    }

    
    public async Task<UserClass> AddUserToClass(int classId, int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new CustomException($"No user with id {userId}");
        }
        var userInClass = await _context.UserClasses.FindAsync(userId,classId);
        if (userInClass != null)
        {
            throw new CustomException($"User with id {userId} already in the class with id {classId}");
            return null;
        }
        var userClass = new UserClass() { ClassId = classId, UserId = userId };
        // kiem tra neu user la student
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        if (userRole.RoleId == 3)
        {
            // add student assignment vao trong bang student assignment
            var assignments = await _context.Assignments.Where(x=> x.ClassId == classId).ToListAsync();
            foreach (var assignment in assignments)
            {
                var studentAssignment = new Data.Entities.StudentAssignment()
                {
                    AssignmentId = assignment.AssignmentId,
                    Feedback = null,
                    Grade = null,
                    StudentId = userId,
                    SubmittedAt = null,
                    Submitted = false
                };
                await _context.StudentAssignments.AddAsync(studentAssignment);
                await _context.SaveChangesAsync();
            }
        }
        await _context.UserClasses.AddAsync(userClass);
        await _context.SaveChangesAsync();
        return userClass;
    }

    public async Task<UserClass> RemoveUserToClass(int classId, int userId)
    {
        var userInClass = await _context.UserClasses.FindAsync(userId,classId);
        if (userInClass == null)
        {
            throw new CustomException($"User with id {userId} not in the class with id {classId}");
            return null;
        }
        _context.UserClasses.Remove(userInClass);
        await _context.SaveChangesAsync();
        return userInClass;
    }

    public async Task<List<User>> GetAllUserInClass(int classId)
    {
        var query = from uc in _context.UserClasses
            where uc.ClassId == classId
            select uc.UserId;
        var listUserInClass = new List<User>();
        foreach (var userId in query)
        {
            var userInClass = await _context.Users.FindAsync(userId);
            
            listUserInClass.Add(userInClass);
        }

        return listUserInClass;
    }

    public async Task<bool> IsUserInClass(int classId, int userId)
    {
        var userInClass = await _context.UserClasses.FindAsync(userId, classId);
        if (userInClass == null)
        {
            return false;
            
        }
        return true;
    }
}