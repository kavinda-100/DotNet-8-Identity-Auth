using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNet_8_Identity_Auth.DTO.Account;
using DotNet_8_Identity_Auth.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DotNet_8_Identity_Auth.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IConfiguration _config;
    
    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _config = configuration;
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
    
    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] RequestLogInDto logInDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // find the user
        var user = await _userManager.FindByEmailAsync(logInDto.Email);
        // check if the user exists
        if (user == null)
        {
            return BadRequest(new {message = "Invalid credentials"});
        }
        // check if the password is correct
        var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
        // check if the password is correct
        if (!result.Succeeded)
        {
            return BadRequest(new {message = "Invalid credentials"});
        }
        // generate the token
        // get the key for the appSettings
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SignInKey"]!));
        // Signing credentials are used to sign the token.
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        // Token descriptor is used to create the token.
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.GivenName, user.FullName),
                // custom claims can be added here
                new("UserId", user.Id)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };
        // Create the token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        // Create the token
        var tokenGen = tokenHandler.CreateToken(tokenDescriptor);
        // Write the token
        var token = tokenHandler.WriteToken(tokenGen);
        
        return Ok(new LogInResponseDto
        {
            Message = "User logged in successfully",
            Body = new BodyLogInResponseDto
            {
                Email = user.Email!,
                Token = token
            }
        });
    }
}