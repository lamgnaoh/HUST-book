using AssignmentApp.Data.EF;
using AssignmentApp.Data.Entities;

namespace AssignmentApp.API.Repository.UserRoles;

public interface IUserRoleRepository

{
    
    Task<List<UserRole>> GetALlRoleForUser(int userId);
    Task<List<UserRole>> CreateUserRole(int userId, int roleId);
    Task<UserRole> updateUserRole(int userId, int oldUserRoleId, int newUSerRoleId);
    Task<List<UserRole>> DeleteUserRole(int userId, int roleId);

}