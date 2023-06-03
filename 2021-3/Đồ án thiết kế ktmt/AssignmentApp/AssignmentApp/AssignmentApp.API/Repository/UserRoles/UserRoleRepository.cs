using AssignmentApp.API.Utilities.Exception;
using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.API.Repository.UserRoles;

public class UserRoleRepository:IUserRoleRepository

{
    private readonly AssignmentAppDbContext _context;

    public UserRoleRepository(AssignmentAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserRole>> GetALlRoleForUser(int userId)
    {
        return await _context.UserRoles.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<List<UserRole>> CreateUserRole(int userId, int RoleId)
    {
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new CustomException($"This user id is not found : {userId}");
            return null;
        }

        var newUserRole = new UserRole()
        {
            UserId = existingUser.Id,
            RoleId = RoleId
        };
        await _context.UserRoles.AddAsync(newUserRole);
        await _context.SaveChangesAsync();
        return await _context.UserRoles.Where(x=> x.UserId == userId).ToListAsync();
    }

    public async Task<UserRole> updateUserRole(int userId, int oldUserRoleId, int newUSerRoleId)
    {
        var existingUserRole = await _context.UserRoles.FindAsync(userId, oldUserRoleId);
        if (existingUserRole == null)
        {
            return null;
        }
        existingUserRole.RoleId = newUSerRoleId;
        await _context.SaveChangesAsync();
        return existingUserRole;
    }

    public async Task<List<UserRole>> DeleteUserRole(int userId, int RoleId)
    {
        var existingUserRole = await _context.UserRoles.FindAsync(userId,RoleId);
        if (existingUserRole == null)
        {
            // throw new CustomException($"No userid {userId} with roleid {RoleId}");
            return null;
        }

        _context.UserRoles.Remove(existingUserRole);
        await _context.SaveChangesAsync();
        return await _context.UserRoles.Where(x=> x.UserId == userId).ToListAsync();
    }
   
}