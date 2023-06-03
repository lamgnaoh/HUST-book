using System.Runtime.InteropServices;
using System.Security.Claims;
using AssignmentApp.API.DTOs;
using AssignmentApp.API.Repository.Classes;
using AssignmentApp.API.Repository.StudentAssignment;
using AssignmentApp.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassController : Controller
{
    private readonly IClassRepository _classRepository;
    private readonly IMapper _mapper;

    public ClassController(IClassRepository classRepository , IMapper mapper )
    {
        _classRepository = classRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetAllClass()
    {
        var classes = await _classRepository.GetAll();
        var classDto = _mapper.Map<List<ClassDto>>(classes);
        return Ok(classDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ActionName("GetClassById")]
    [Authorize]
    public async Task<IActionResult> GetClassById(int id)
    {
        var existingClass = await _classRepository.GetClass(id);
        if (existingClass == null)
        {
            return NotFound($"Cannnot find class with id : {id}");
            
        }

        var existingClassDto = _mapper.Map<ClassDto>(existingClass);
        return Ok(existingClassDto);
    }

    [HttpGet]
    [Route("user")]
    [Authorize(Roles = "2,3")]
    public async Task<IActionResult> GetAllClassAttended()
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userID = Int32.Parse(idClaim);
        var classAttends = await _classRepository.GetALlAttended(userID);
        var classAttendsDto = _mapper.Map<List<ClassDto>>(classAttends);
        return Ok(classAttendsDto);
    }
    
    

    [HttpPost]
    [Authorize(Roles= "2,1" )]
    public async Task<IActionResult> CreateClass(ClassCreateRequestDTO request)
    {
        var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = Int32.Parse(idClaim);
        var newClass = new Class()
        {
            Name = request.Name,
            CreateAt = DateTime.Now,
            // UserCreateId = id
        };
        var response = await _classRepository.CreateClass(newClass);
        var classDto = _mapper.Map<ClassDto>(response);
        return CreatedAtAction(nameof(GetClassById), new { id = response.ClassId }, classDto);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "2,1")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        //get assignment from database , if null  return not found 
        var deleteClass = await _classRepository.DeleteClass(id);
        if (deleteClass == null)
        {
            return NotFound();
        }
        // convert response to Dto
        var deleteClassDto = _mapper.Map<ClassDto>(deleteClass);
        return Ok(deleteClassDto);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "2,1")]
    public async Task<IActionResult> UpdateClass([FromRoute] int id,[FromBody] ClassUpdateRequestDto request)
    {
        var existingClass = await _classRepository.GetClass(id);
        if (existingClass == null)
        {
            return NotFound();
            
        }

        var classId = existingClass.ClassId;
        var createAt = existingClass.CreateAt;
        existingClass = new Class()
        {
            ClassId = classId,
            Name = request.Name,
            CreateAt = createAt
        };
        var updateClass = await _classRepository.UpdateClass(existingClass, id);
        if (updateClass == null)
        {
            return BadRequest();
        }

        var updateClassDto = _mapper.Map<ClassDto>(updateClass);
        return Ok(updateClassDto);
    }

    // chi admin moi co quyen add user vao trong tat ca class . con teacher day class nao moi co quyen add user vao trong class do 
    [HttpPost]
    [Route("{classId:int}/users/{userId:int}")]
    [Authorize(Roles = "2,1")]
    public async Task<IActionResult> AddUserToClass( int classId, int userId)
    {
        var IdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var roleId = Int32.Parse(roleClaim);
        if (roleId != 1)
        {
            var Id = Int32.Parse(IdClaim);
            var isUserInClass =  await _classRepository.IsUserInClass(classId, Id);
            if (!isUserInClass)
            {
                return BadRequest($"You cannot add another user to class with id {classId} because you not in class");
            }
        }
        var userClass = await _classRepository.AddUserToClass(classId,userId);
        if (userClass == null)
        {
            return BadRequest($"Can not add user with id {userId} to class with id {classId}");
        }

        var userClassDto = _mapper.Map<UserClassDto>(userClass);
        return Ok(userClassDto);
    }
    
    // chi admin moi co quyen remove user khoi  tat ca class . con teacher day class nao moi co quyen remove user  trong class do 

    [HttpDelete]
    [Route("{classId:int}/users/{userId:int}")]
    [Authorize(Roles = "2,1")]
    public async Task<IActionResult> RemoveUserToClass( int classId, int userId)
    {
        var IdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var roleId = Int32.Parse(roleClaim);
        if (roleId != 1)
        {
            var Id = Int32.Parse(IdClaim);
            var isUserInClass =  await _classRepository.IsUserInClass(classId, Id);
            if (!isUserInClass)
            {
                return BadRequest($"You cannot add another user to class with id {classId} because you not in class");
            }
        }
        var userClass = await _classRepository.RemoveUserToClass(classId,userId);
        if (userClass == null)
        {
            return BadRequest($"Can not remove user with id {userId} to class with id {classId}");
        }

        var userInClass = await _classRepository.GetAllUserInClass(classId);
        var userInClassDto = _mapper.Map<List<UserDto>>(userInClass);
        return Ok(userInClassDto);
    }

    // chi admin moi co quyen get list user  trong tat ca class . con user nao trong   class nao moi co quyen get list  user  trong class do 

    [HttpGet]
    [Route("{classId:int}/users")]
    [Authorize]
    public async Task<IActionResult> GetAllUserInClass(int classId)
    {
        var IdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var roleClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var roleId = Int32.Parse(roleClaim);
        //neu ng dang nhap k la admin
        if (roleId != 1)
        {
            //id nguoi dang nhap
            var Id = Int32.Parse(IdClaim);
            var isUserInClass =  await _classRepository.IsUserInClass(classId, Id);
            if (!isUserInClass)
            {
                return Ok(new EmptyResult());
            }
        }
        //neu ng dang nhap la admin
        var users = await _classRepository.GetAllUserInClass(classId);
        var usersDto = _mapper.Map<List<UserDto>>(users);
        return Ok(usersDto);
    }
}