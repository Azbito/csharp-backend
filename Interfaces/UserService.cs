using backend.DTOs;
using backend.Models;

namespace backend.Interfaces;

public interface IUserService
{
    User? Create(CreateUser dto);
    IEnumerable<User> GetUsers();
    User? GetUserById(string id);
}
