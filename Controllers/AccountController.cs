using DotNet_8_Identity_Auth.DTO.Account;
using DotNet_8_Identity_Auth.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_8_Identity_Auth.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RequestRegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // create the user
        var user = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FullName = registerDto.FullName
        };
        // save the user
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        // check if the user was created
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(new {message = "User created successfully"});
    }
}