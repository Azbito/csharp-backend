using backend.DTOs;
using backend.Interfaces;
using backend.Models;

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
}
