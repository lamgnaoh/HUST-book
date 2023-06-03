using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}