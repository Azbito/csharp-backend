using backend.DTOs;
using backend.Models;

namespace backend.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    IEnumerable<User> GetAll();
    User? GetById(Guid id);
    User? GetByEmail(string email);
    UserResponse? GetByUsername(string username);
}
