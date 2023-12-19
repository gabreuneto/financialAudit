// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILoginService _loginService; // Alterado para ILoginService
    private readonly IConfiguration _configuration;

    public AuthController(ILoginService loginService, IConfiguration configuration)
    {
        _loginService = loginService; // Alterado para ILoginService
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] Login model)
    {
        // Valide as credenciais usando o serviço de login
        var user = _loginService.ValidateCredentials(model.Username, model.Password);

        if (user != null)
        {
            // Gere um token JWT
            var token = GenerateJwtToken(user.UserId, user.Name);
            return Ok(new { Token = token });
        }

        return Unauthorized(new { Message = "Credenciais inválidas" });
    }

    private string GenerateJwtToken(int userId, string username)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiration = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:HoursToExpire"]));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, username)
            }),
            Expires = expiration,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
