using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using backend.Repositories;
using BCrypt.Net;

namespace backend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User Create(CreateUser dto)
    {
        try
        {
            if (
                string.IsNullOrEmpty(dto.Password)
                || string.IsNullOrEmpty(dto.Email)
                || string.IsNullOrEmpty(dto.Name)
            )
            {
                throw new ArgumentException("All the fields must have a value.");
            }

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
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Input error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating user: {ex.Message}");
            throw;
        }
    }

    public IEnumerable<User> GetUsers() => _repository.GetAll();

    public User? GetUserById(Guid id) => _repository.GetById(id);

    public UserData? GetUserByUsername(string username) => _repository.GetByUsername(username);
}
