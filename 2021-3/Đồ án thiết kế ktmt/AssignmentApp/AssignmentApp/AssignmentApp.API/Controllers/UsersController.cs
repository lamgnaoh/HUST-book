using System.Security.Claims;
using AssignmentApp.API.DTOs;
using AssignmentApp.API.Repository.UserRoles;
using AssignmentApp.API.Repository.Users;
using AssignmentApp.API.Utilities.Paging;
using AssignmentApp.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController:Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper ,IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
    }

    [HttpGet]
    [Authorize(Roles = "2")]
    [Route("students")]
    public async Task<IActionResult> GetAllStudent([FromQuery]UserPagingParameter pagingParameter)
    {
        var students = await _userRepository.GetAllStudent(pagingParameter);
        var studentsDtos = new List<UserDto>();
        foreach (var student in students)
        {
            
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
                studentsDtos.Add(studentDto);
        }
        return Ok(studentsDtos);
    }

    [HttpGet]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> GetAll([FromQuery] UserPagingParameter pagingParameter)
    {
        var users = await _userRepository.GetAll(pagingParameter);
        var usersDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var userRoles = await _userRoleRepository.GetALlRoleForUser(user.Id);
            List<string> roles = new List<string>();
            foreach (var userRole in userRoles)
            {
                if (userRole.RoleId == 1)
                {
                    roles.Add("admin");
                }

                if (userRole.RoleId == 2)
                {
                    roles.Add("teacher");
                }

                if (userRole.RoleId == 3)
                {
                    roles.Add("student");
                }

                var userDto = new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    MSSV = user.MSSV,
                    FullName = user.FullName,
                    roles = roles
                };
                usersDtos.Add(userDto);
            }
        }

        return Ok(usersDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ActionName("GetUserById")]
    [Authorize]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound($"Not found user with id {id}");
        }

        var userRoles = await _userRoleRepository.GetALlRoleForUser(user.Id);
        List<int> roleIds = new List<int>();
        foreach (var userRole in userRoles)
        {
            roleIds.Add(userRole.RoleId);
        }
        var userGetRequestDto = new UserGetRequestDto()
        {
            Id = user.Id,
            Username = user.Username,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            MSSV = user.MSSV,
            FullName = user.FullName,
            RoleIds = roleIds
        };
        return Ok(userGetRequestDto);
    }

    [HttpPost]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var newUser = new User()
        {
            Username = request.Username,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            MSSV = request.MSSV,
            FullName = request.FullName
        };
        var RoleIds = request.RoleIDs;
        newUser = await _userRepository.CreateUser(newUser,RoleIds);
        var newUserDto = _mapper.Map<UserDto>(newUser);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUserDto);
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "1")]
    // admin update user info
    public async Task<IActionResult> UpdateUser([FromRoute] int id,
        [FromBody] UserUpdateRequestDto userUpdateRequestDto)
    {
        // convert dto to domain model 
        var updateUser = await _userRepository.GetUserById(id);
        if (updateUser == null)
        {
            return NotFound();
        }

        updateUser = new User()
        {
            Id = updateUser.Id,
            Username = userUpdateRequestDto.Username,
            Password = updateUser.Password,
            Email = userUpdateRequestDto.Email,
            PhoneNumber = userUpdateRequestDto.PhoneNumber,
            FullName = userUpdateRequestDto.FullName,
            MSSV = userUpdateRequestDto.MSSV
        };
        updateUser = await _userRepository.UpdateUser(updateUser ,updateUser.Id);
        var userRoles = await _userRoleRepository.GetALlRoleForUser(updateUser.Id);
        List<string> roles = new List<string>();
        foreach (var userRole in userRoles)

        {
            foreach (var role in userUpdateRequestDto.RoleIDs)
            {

                await _userRoleRepository.DeleteUserRole(updateUser.Id, userRole.RoleId);
                var newUserRoles = await _userRoleRepository.CreateUserRole(updateUser.Id, role);

                foreach (var newUserRole in newUserRoles)
                {
                    if (newUserRole.RoleId == 1)
                    {
                        roles.Add("admin");
                    }
                    if (newUserRole.RoleId == 2)
                    {
                        roles.Add("teacher");
                    }
                    if (newUserRole.RoleId == 3)
                    {
                        roles.Add("student");
                    }
                }
            }
            
        }
        var updateUserDto = new UserDto()
        {
            Id = updateUser.Id,
            Username = updateUser.Username,
            Password = updateUser.Password,
            PhoneNumber = updateUser.PhoneNumber,
            Email = updateUser.Email,
            MSSV = updateUser.MSSV,
            FullName = updateUser.FullName,
            roles = roles
        };
            return Ok(updateUserDto);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        //get assignment from database , if null  return not found 
        var deleteUser = await _userRepository.DeleteUser(id);
        if (deleteUser == null)
        {
            return BadRequest();
        }
        // convert response to Dto
        var deleteUserDto = _mapper.Map<UserDto>(deleteUser);
        return Ok(deleteUserDto);
    }

    [HttpGet]
    [Route("search")]
    [Authorize]
    public async Task<IActionResult> GetUserByUsername([FromQuery]string keyword)
    {
        var users = await _userRepository.GetUserByUserName(keyword);
        if (users == null)
        {
            return NotFound($"Cannot find username with the keyword: {keyword}");
        }

        var userDto = _mapper.Map<List<UserDto>>(users);
        return Ok(userDto);
    }
    
    // Update me- khong the update role
    [HttpPost]
    [Route("me")]
    [Authorize]
    public async Task<IActionResult> UpdateMe([FromBody] UserUpdateMeRequestDto updateRequestDto)
    {
        var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Int32.Parse(idClaim);
        var updateUser = await _userRepository.GetUserById(id);
        if (updateUser == null)
        {
            return NotFound($"No User with id {id}");
        }
        updateUser = new User()
        {
            Username = updateUser.Username,
            Password = updateRequestDto.Password,
            Email = updateUser.Email,
            PhoneNumber = updateUser.PhoneNumber,
            FullName = updateUser.FullName,
            MSSV =updateUser.MSSV
        };
        updateUser = await _userRepository.UpdateUser(updateUser, id);
        if (updateUser == null)
        {
            return BadRequest("Cannot update user");
        }
        
        var updateUserDto = _mapper.Map<UserDto>(updateUser);
        return Ok(updateUserDto);
    }
}