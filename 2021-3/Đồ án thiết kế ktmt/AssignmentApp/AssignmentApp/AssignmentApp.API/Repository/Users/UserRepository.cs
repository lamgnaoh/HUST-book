using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssignmentApp.API.DTOs;
using AssignmentApp.API.Repository.Token;
using AssignmentApp.API.Utilities.Exception;
using AssignmentApp.API.Utilities.Paging;
using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AssignmentApp.API.Repository.Users;

public class UserRepository : IUserRepository
{
    

    
    private readonly AssignmentAppDbContext _context;
    private readonly ITokenHandler _iTokenHandler;

    public UserRepository( AssignmentAppDbContext context , ITokenHandler iTokenHandler)
    {
        _context = context;
        _iTokenHandler = iTokenHandler;
    }
    public async Task<string> Authenticate(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
        if (user == null)
        {
            throw new CustomException("No account belong to this email.Try again");
            return null;
        }
        if (user.Password.Equals(request.Password) != true)
        {
            throw new CustomException("Wrong password");
            return null;
        }
        var token = await _iTokenHandler.CreateTokenHanlder(user,userRoles);
        return token;
    }

    public async Task<bool> Register(RegisterRequest request)
    {
        var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (checkUser != null)
        {
            return false;
        }
        var user = new User()
        {
            Username = request.Username,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            MSSV = request.MSSV,
            FullName = request.FullName,
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        var userRole = new UserRole()
        {
            UserId = user.Id,
            RoleId = request.RoleId
        };
        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<User>> GetAll(UserPagingParameter pagingParameter)
    {
        var users = await _context.Users.OrderBy(u => u.Username)
            .Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize).Take(pagingParameter.PageSize)
            .ToListAsync();
        return users;
    }

    public async Task<List<User>> GetAllStudent(UserPagingParameter pagingParameter)
    {
        var studentids = from u in _context.Users
            join ur in _context.UserRoles on u.Id equals ur.UserId
            where ur.RoleId == 3
            select u.Id;
        List<User> students = new List<User>();
        foreach (var studentid in studentids)
        {
            var student = await _context.Users.FindAsync(studentid);
            students.Add(student);
        }

        var listStudents = students.Skip((pagingParameter.PageNumber - 1) * pagingParameter.PageSize).Take(pagingParameter.PageSize).ToList();
        return listStudents;
    }

    public async Task<User> CreateUser(User createUser, List<int> RoleIDs)
    {
        var newUser = new User()
        {
            Username = createUser.Username,
            Password = createUser.Password,
            PhoneNumber = createUser.PhoneNumber,
            Email = createUser.Email,
            MSSV = createUser.MSSV,
            FullName = createUser.FullName,
        };
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        foreach (var roleID in RoleIDs)
        {
            var newUserRole = new UserRole()
            {
                UserId = newUser.Id,
                RoleId = roleID
            };
            await _context.UserRoles.AddAsync(newUserRole);
        }
         await _context.SaveChangesAsync();
         return newUser;

    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> UpdateUser(User user , int userId)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new CustomException($"This assignment id is not found : {userId}");
            return null;
        }

        existingUser.Username= user.Username;
        existingUser.Password= user.Password;
        existingUser.PhoneNumber= user.PhoneNumber;
        existingUser.Email= user.Email;
        existingUser.MSSV= user.MSSV;
        existingUser.FullName= user.FullName;
        // existingUser.RoleId= user.RoleId;
        await _context.SaveChangesAsync();
        return existingUser;
    }

  
    // public async Task<User> UpdateRole(int userId,int roleId)
    // {
    //     var existingUser = await _context.Users.FindAsync(userId);
    //     if (existingUser == null)
    //     {
    //         throw new CustomException($"This assignment id is not found : {userId}");
    //         return null;
    //     }
    //
    //     foreach (var roleId in roles)
    //     {
    //         var existingUserRole = await _context.UserRoles.FindAsync(userId , roleId);
    //         if (existingUserRole == null)
    //         {
    //             return null;
    //         }
    //         existingUserRole.RoleId = roleId;
    //         await _context.SaveChangesAsync();
    //     }
    //     await _context.SaveChangesAsync();
    //     return existingUser;
    // }

    public async Task<User> DeleteUser(int id)
    {
        var deleteUser = await _context.Users.FindAsync(id);
        if (deleteUser == null)
        {
            throw new CustomException($"This user is not found with that id : {id}");
            return null;
        }

        var userClasses = await _context.UserClasses.Where(x => x.UserId == id).ToListAsync();
        _context.UserClasses.RemoveRange(userClasses);
        _context.Users.Remove(deleteUser);
        await _context.SaveChangesAsync();
        return deleteUser;
    }

    public async Task<List<User>> GetUserByUserName(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
        {
            return null;
        }
        var users = await _context.Users.Where(x=> x.Username.Contains(keyword)).ToListAsync();
        return users;
    }
}