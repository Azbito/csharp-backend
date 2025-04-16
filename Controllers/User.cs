using backend.DTOs;
using backend.Interfaces;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class UserController : IUserControllers
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    public IResult CreateUser(CreateUser dto)
    {
        var user = _service.Create(dto);

        var result = new
        {
            user.Id,
            user.Name,
            user.Email,
            user.UserName,
        };

        return Results.Created($"/users/{user.Id}", result);
    }

    public IResult GetAllUsers()
    {
        var users = _service
            .GetUsers()
            .Select(u => new
            {
                u.Id,
                u.Name,
                u.Email,
            });

        return Results.Ok(users);
    }

    public IResult GetUserById(Guid id)
    {
        var user = _service.GetUserById(id);
        if (user is null)
            return Results.NotFound();

        var result = new
        {
            user.Id,
            user.Name,
            user.Email,
        };

        return Results.Ok(result);
    }

    public IResult GetUserByUsername(string username)
    {
        var user = _service.GetUserByUsername(username);
        if (user is null)
            return Results.NotFound();

        return Results.Ok(user);
    }
}
