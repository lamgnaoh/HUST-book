using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class UserClassProfile:Profile   
{
    public UserClassProfile()
    {
        CreateMap<UserClass, UserClassDto>();
    }
}