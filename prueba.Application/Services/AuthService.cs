using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using prueba.Domain.Entities;
using prueba.Domain.Interface;

namespace prueba.Application.Services;

public class AuthService
{
    private readonly IUserRepositorie _repositorie;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepositorie repositorie, IConfiguration configuration)
    {
        _repositorie = repositorie;
        _configuration = configuration;
    }

    public async Task<bool> Register(string username, string password, string role)
    {
        var existingUser = await _repositorie.GetUserByUsernameAsync(username);
        if (existingUser != null)
            return false;

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };
        await _repositorie.RegisterUser(user);
        return true;
    }

    public async Task<string?> Authenticate(string username, string password)
    {
        var user = await _repositorie.GetUserByUsernameAsync(username);
        if (user == null) return null;
        
        //Verificar Hash con bcrypt
        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if(!passwordValid) return null;
        
        //Generar token claims 
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer:_configuration["Jwt:Issuer"],
            audience:_configuration["Jwt:Audience"],
            claims: claims,
            expires:DateTime.Now.AddMinutes(30),
            signingCredentials: cred);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}