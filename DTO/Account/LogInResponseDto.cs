namespace DotNet_8_Identity_Auth.DTO.Account;

public class LogInResponseDto
{
    public String Message { get; set; } = String.Empty;
    public BodyLogInResponseDto Body { get; set; } = new BodyLogInResponseDto();
}

public class BodyLogInResponseDto
{
    public String Email { get; set; } = String.Empty;
    public String Token { get; set; } = String.Empty;
}