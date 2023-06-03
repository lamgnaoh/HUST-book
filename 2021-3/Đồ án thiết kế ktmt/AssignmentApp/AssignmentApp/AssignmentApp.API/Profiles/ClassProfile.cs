using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class ClassProfile: Profile
{
    public ClassProfile()
    {
        CreateMap<Class, ClassDto>().ReverseMap();
    }
}