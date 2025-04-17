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
        var validator = new CreateUserValidator();
        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(new { message = "❌ Validation failed", errors });
        }

        var user = _service.Create(dto);

        if (user == null)
        {
            return Results.Conflict(new { message = "❌ User already exists" });
        }

        var result = new
        {
            user.Id,
            user.Name,
            user.Email,
            user.UserName,
            user.CreatedAt,
            user.UpdatedAt,
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
