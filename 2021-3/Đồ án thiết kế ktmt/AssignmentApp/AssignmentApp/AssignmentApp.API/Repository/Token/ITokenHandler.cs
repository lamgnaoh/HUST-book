using AssignmentApp.Data.Entities;

namespace AssignmentApp.API.Repository.Token;

public interface ITokenHandler
{
    Task<string> CreateTokenHanlder(User user, List<UserRole> userRoles);
}