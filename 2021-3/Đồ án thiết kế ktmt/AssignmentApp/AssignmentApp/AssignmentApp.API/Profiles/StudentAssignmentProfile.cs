using AssignmentApp.API.DTOs;
using AssignmentApp.Data.Entities;
using AutoMapper;

namespace AssignmentApp.API.Profiles;

public class StudentAssignmentProfile:Profile
{
    public StudentAssignmentProfile()
    {
        CreateMap<StudentAssignment, StudentAssignmentDto>();
    }
}