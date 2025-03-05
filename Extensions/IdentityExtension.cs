using DotNet_8_Identity_Auth.data;
using DotNet_8_Identity_Auth.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DotNet_8_Identity_Auth.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddIdentityHandlersAndStoresExtension(this IServiceCollection services)
    {
        // Identity configuration
        services.AddIdentityApiEndpoints<AppUser>()
            .AddRoles<IdentityRole>() // add roles
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        return services;
    }
    
    public static IServiceCollection AddConfigureIdentityOptionsExtension(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings for the user (in this case, keeping it simple)
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            // for the email
            options.User.RequireUniqueEmail = true;
        });
        
        return services;
    }

    public static IServiceCollection AddIdentityAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.SaveToken = false;
            // token validation parameters
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SignInKey"]!))
            };
        });
        // add authorization
        // services.AddAuthorization(options =>
        // {
        //     options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        //     options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
        // });
        return services;
    }
}