using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class UserRoleProfile:Profile
{
    public UserRoleProfile()
    {
        CreateMap<UserRole, UserRolesDto>();
    }
}