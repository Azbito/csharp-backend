using backend.Config;
using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetById(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public UserResponse? GetByUsername(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == username);

        if (user == null)
            return null;

        var userDto = new UserResponse
        {
            UserName = user.UserName,
            Name = user.Name,
            Email = user.Email,
        };

        return userDto;
    }
}
