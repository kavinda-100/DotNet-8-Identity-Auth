using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DotNet_8_Identity_Auth.models;

public class AppUser: IdentityUser
{
    [PersonalData]
    [Column(TypeName = "varchar(255)")]
    public String FullName { get; set; } = String.Empty;
}