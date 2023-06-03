using AssignmentApp.API.DTOs;
using AssignmentApp.API.Utilities.Paging;
using AssignmentApp.Data.Entities;

namespace AssignmentApp.API.Repository.Users;

public interface IUserRepository
{
    Task<string> Authenticate(LoginRequest request);
    Task<bool> Register(RegisterRequest request);

    Task<List<User>> GetAll(UserPagingParameter pagingParameter);
    Task<List<User>> GetAllStudent(UserPagingParameter pagingParameter);
    Task<User> CreateUser(User createUser, List<int> RoleIDs);

    Task<User> GetUserById(int id);

    Task<User> UpdateUser(User user , int userId);
    // Task<User> UpdateRole(int userId , List<int> roles);

    Task<User> DeleteUser(int id);

    Task<List<User>> GetUserByUserName(string keyword);
    
}