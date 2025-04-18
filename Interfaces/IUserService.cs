using backend.DTOs;
using backend.Models;

namespace backend.Interfaces;

public interface IUserService
{
    User? Create(DTOCreateUser dto);
    IEnumerable<User> GetUsers();
    User? GetUserById(string id);
    User CreateFromOAuth(string name, string email);
    User? GetByEmail(string email);
    string GenerateJwt(User user);
}
