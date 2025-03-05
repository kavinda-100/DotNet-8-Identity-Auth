using DotNet_8_Identity_Auth.Extensions;
using DotNet_8_Identity_Auth.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_8_Identity_Auth.Controllers;

[ApiController]
[Route("api/user-profile")]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    
    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        string? email = User.GetUserEmail();
        if(string.IsNullOrEmpty(email)) return StatusCode(500, new { message = "User not found" });
        
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(new { message = "User profile", user });
    }
}