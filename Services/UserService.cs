using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User? Create(DTOCreateUser dto)
    {
        var existingUser = _repository.GetByUsername(dto.UserName);

        if (existingUser != null)
            return null;

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = passwordHash,
            UserName = dto.UserName,
        };

        _repository.Add(user);
        return user;
    }

    public IEnumerable<User> GetUsers() => _repository.GetAll();

    public User? GetUserById(string id) => _repository.GetById(id);

    public UserResponse? GetUserByUsername(string username) => _repository.GetByUsername(username);

    public User? GetByEmail(string email)
    {
        return _repository.GetByEmail(email);
    }

    public User CreateFromOAuth(string name, string email)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            UserName = email.Split('@')[0],
            Password = "",
        };

        _repository.Add(user);
        return user;
    }

    public string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? "");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                }
            ),
            Expires = DateTime.UtcNow.AddDays(31),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
