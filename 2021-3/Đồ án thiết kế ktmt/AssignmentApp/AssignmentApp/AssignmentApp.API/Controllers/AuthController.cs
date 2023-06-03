using System.Security.Claims;
using System.Security.Cryptography;
using AssignmentApp.API.DTOs;
using AssignmentApp.API.Repository.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AssignmentApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var resultToken = await _userRepository.Authenticate(loginRequest);
        if (string.IsNullOrEmpty(resultToken))
        {
            return BadRequest("Email or password is not correct");
        }
        
        //Get list of claim
        
        return Ok(resultToken);
        
        

    }

    [HttpGet]
    [Route("user-info")]
    [Authorize]
    public async Task<IActionResult> GetUserInfoFromToken()
    {
        var fullName = User.FindFirstValue("Name");
        var roleClaims = User.Claims.Where(x=> x.Type == ClaimTypes.Role).ToList();
        IList<string> roles = new List<string>();
        foreach (var roleClaim in roleClaims)
        {
            var roleId = roleClaim.Value;
            string role = null;
            if (roleId =="1")
            {
                role = "admin";
            } else if (roleId == "2")
            {
                role = "teacher";
            } else if (roleId == "3")
            {
                role = "student";
            }
            
            roles.Add(role);
        }

       string stringrole =  string.Join(" ", roles.ToArray());
        var email = User.FindFirstValue("Email");
        var username = User.FindFirstValue("Name");
        var phoneNumber = User.FindFirstValue("PhoneNumber");
        var mssv = User.FindFirstValue("MSSV");
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var password = User.FindFirstValue("Password");
        return Ok(new {fullName,
            stringrole,
            email,username,phoneNumber,mssv,userId,password});
    }
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _userRepository.Register(registerRequest);
        if (result == false)
        {
            return BadRequest("Register unsuccessfull");
        }

        return Ok();
    }
}