using System.Security.Claims;
using AssignmentApp.API.DTOs;
using AssignmentApp.API.Repository.Assignments;
using AssignmentApp.API.Repository.Classes;
using AssignmentApp.API.Repository.Files;
using AssignmentApp.API.Repository.StudentAssignment;
using AssignmentApp.API.Repository.Users;
using AssignmentApp.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentAssignmentController : Controller
{
    private readonly IStudentAssignmentRepository _studentAssignmentRepository;
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IClassRepository _classRepository;
    private readonly IUserRepository _userRepository;
    private readonly IFileRepository _fileRepository;

    private readonly IMapper _mapper;

    public StudentAssignmentController(IStudentAssignmentRepository studentAssignmentRepository , IMapper mapper,IClassRepository classRepository, IAssignmentRepository assignmentRepository, IUserRepository userRepository, IFileRepository fileRepository)
    {
        _studentAssignmentRepository = studentAssignmentRepository;
        _classRepository = classRepository;
        _assignmentRepository = assignmentRepository;
        _userRepository = userRepository;
        _fileRepository = fileRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "3")]
    public async Task<IActionResult> GetAllAssignedAssignment()
    {
        var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Int32.Parse(idClaim);
        var studentAssignments = await _studentAssignmentRepository.GetAllAssignedAssignment(id);
        
        List<StudentAssignmentDto> studentAssignmentDtos = new List<StudentAssignmentDto>();
        foreach (var studentAssignment in studentAssignments)
        {
            var student = await _userRepository.GetUserById(studentAssignment.StudentId);
            var studentDto = new UserDto()
            {
                Id = student.Id,
                Username = student.Username,
                Password = student.Password,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                MSSV = student.MSSV,
                FullName = student.FullName,
                roles = new List<string>(){"student"}
            };
            
            var studentAssigmentDto = new StudentAssignmentDto()
            {
                AssignmentId = studentAssignment.AssignmentId,
                Feedback = studentAssignment.Feedback,
                Grade = studentAssignment.Grade,
                Student = studentDto,
                Submitted = studentAssignment.Submitted,
                SubmittedAt = studentAssignment.SubmittedAt,
                // SubmitFiles = 
            };
            studentAssignmentDtos.Add(studentAssigmentDto);
            
        }
        return Ok(studentAssignmentDtos);
    }

    [HttpGet]
    [Route("Assignments/{AssignmentId:int}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> GetAllStudentAssignmentForAssignment(int AssignmentId)
    {
        //get assignment
        var assignment = await _assignmentRepository.GetAssignment(AssignmentId);
        var IdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //get teacher id 
        var Id = Int32.Parse(IdClaim);
        var isUserInClass =  await _classRepository.IsUserInClass(assignment.ClassId, Id);
        if (!isUserInClass)
        {
            return BadRequest($"You cannot get student assignment to assignment  id {AssignmentId} because you not in class");
        }
        //lấy tất cả student assignment của assignment có id AssignmentId
        var studentAssignments = await _studentAssignmentRepository.GetAllStudentAssignmentForAssignment(AssignmentId);
        List<StudentAssignmentDto> studentAssignmentDtos = new List<StudentAssignmentDto>();
        foreach (var studentAssignment in studentAssignments)
        {
            var files = await _fileRepository.GetFileFromStudentAssignment(AssignmentId, studentAssignment.StudentId);
            List<FileDto> fileDtos = new List<FileDto>();
            foreach (var item in files)
            {
                var fileDto = new FileDto()
                {
                    FileId = item.FileId,
                    Name = item.Name,
                    Path = item.Path
                };
                fileDtos.Add(fileDto);
            }
            var student = await _userRepository.GetUserById(studentAssignment.StudentId);
            var studentDto = new UserDto()
            {
                Id = student.Id,
                Username = student.Username,
                Password = student.Password,
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                MSSV = student.MSSV,
                FullName = student.FullName,
                roles = new List<string>(){"student"}
            };
            
            var studentAssigmentDto = new StudentAssignmentDto()
            {
                AssignmentId = AssignmentId,
                Feedback = studentAssignment.Feedback,
                Grade = studentAssignment.Grade,
                Student = studentDto,
                Submitted = studentAssignment.Submitted,
                SubmittedAt = studentAssignment.SubmittedAt,
                SubmitFiles = fileDtos
            };
            studentAssignmentDtos.Add(studentAssigmentDto);
            
        }
        return Ok(studentAssignmentDtos);
    }

    [HttpPut]
    [Route("Assignments/{AssignmentId:int}/Student/{studentId:int}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> GradeStudentAssignment(int AssignmentId, int studentId,
        AssignmentMarkCreateDto markCreateDto)
    {
        //lấy assignment
        var assignment = await _assignmentRepository.GetAssignment(AssignmentId);
        // kiem tra xem teacher có trong class để chấm assignment student nộp không
        var IdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var Id = Int32.Parse(IdClaim);
        var isUserInClass =  await _classRepository.IsUserInClass(assignment.ClassId, Id);
        if (!isUserInClass)
        {
            return BadRequest($"You cannot grade student assignment to assignment id {AssignmentId} because you not in class");
        }
        // lấy assignment student nộp
        var studentAssignment = await _studentAssignmentRepository.GetStudentAssignment(AssignmentId, studentId);
        if (studentAssignment == null)
        {
            return NotFound();
        }
        
        studentAssignment = new StudentAssignment()
        {
            AssignmentId = studentAssignment.AssignmentId,
            StudentId = studentAssignment.StudentId,
            Submitted = studentAssignment.Submitted,
            SubmittedAt = studentAssignment.SubmittedAt,
            Grade = markCreateDto.Grade,
            Feedback = markCreateDto.Feedback
        };
        
        var response = await _studentAssignmentRepository.GradeAssignment(studentAssignment, AssignmentId, studentId);
        if (response == null)
        {
            return BadRequest($"student with id {studentId}  does not submit assignment with id {AssignmentId} or dont have assignment id {AssignmentId} assign to student {studentId}");
        }
        //get file from student assignment
        var files = await  _fileRepository.GetFileFromStudentAssignment(AssignmentId, studentId);
        List<FileDto> fileDtos = new List<FileDto>();
        foreach (var item in files)
        {
            var fileDto = new FileDto()
            {
                FileId = item.FileId,
                Name = item.Name,
                Path = item.Path
            };
            fileDtos.Add(fileDto);
        }
        var student = await _userRepository.GetUserById(studentAssignment.StudentId);
        var studentDto = new UserDto()
        {
            Id = student.Id,
            Username = student.Username,
            Password = student.Password,
            PhoneNumber = student.PhoneNumber,
            Email = student.Email,
            MSSV = student.MSSV,
            FullName = student.FullName,
            roles = new List<string>(){"student"}
        };
        
        var studentAssigmentDto = new StudentAssignmentDto()
        {
            AssignmentId = AssignmentId,
            Feedback = studentAssignment.Feedback,
            Grade = studentAssignment.Grade,
            Student = studentDto,
            Submitted = studentAssignment.Submitted,
            SubmittedAt = studentAssignment.SubmittedAt,
            SubmitFiles = fileDtos
        };
            
        
        return Ok(studentAssigmentDto);
    }
}