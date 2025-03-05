using DotNet_8_Identity_Auth.Extensions;
using DotNet_8_Identity_Auth.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_8_Identity_Auth.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    
    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet("get-user")]
    [Authorize]
    public async Task<IActionResult> GetUserProfile()
    {
        string? email = User.GetUserEmail();
        if(string.IsNullOrEmpty(email)) return StatusCode(500, new { message = "User not found" });
        
        var user = await _userManager.FindByEmailAsync(email);
        if(user == null) return StatusCode(500, new { message = "User not found" });
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var userObj = new
        {
            user?.Id,
            user?.Email,
            user?.FullName,
            Roles = roles.First()
        };
        return Ok(new { message = "User profile", user = userObj });
    }
    
    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")] // for a single role
    public IActionResult GetAdminProfile()
    {
        return Ok(new { message = "Admin profile" });
    }
    
    [HttpGet("for-all")]
    [Authorize(Roles = "User, Admin")] // for multiple roles
    public IActionResult GetForAll()
    {
        return Ok(new { message = "For all" });
    }
    
    [HttpGet("for-admin-or-user-via-policy")]
    [Authorize(Policy = "RequireAdminRole")] // for a single policy same as [Authorize(Roles = "Admin")]
    public IActionResult GetForAdminOrUserViaPolicy()
    {
        return Ok(new { message = "For admin or user via policy" });
    }
}