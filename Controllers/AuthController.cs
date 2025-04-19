using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class AuthController : ControllerBase, IAuthController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IResult LoginWithGoogle(HttpContext context)
    {
        return _authService.LoginWithGoogle(context);
    }

    public IResult Login(DTOLogin dto)
    {
        var validator = new LoginValidator();
        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Results.BadRequest(new { message = "❌ Validation failed", errors });
        }

        return _authService.Login(dto);
    }

    public Task<IResult> HandleGoogleCallback(HttpContext context)
    {
        return _authService.HandleGoogleCallback(context);
    }
}
