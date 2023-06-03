using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class AssigmentsProfile : Profile
{
    public AssigmentsProfile()
    {
        CreateMap<Assignment, AssignmentDto>()
            .ForMember(dest => dest.Id , options =>options.MapFrom(src => src.AssignmentId));// map từ assignmentId của assignment sang id của assignmentDto
    }
}