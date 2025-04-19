using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace backend.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    public AuthService(IUserService userService)
    {
        _userService = userService;
    }

    public IResult Login(DTOLogin dto)
    {
        var user = _userService.GetByEmail(dto.Email);

        if (user == null)
            return Results.Unauthorized();

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return Results.Unauthorized();

        var token = _userService.GenerateJwt(user);

        return Results.Ok(
            new
            {
                message = "✅ Login successful",
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                },
                token,
            }
        );
    }

    public IResult LoginWithGoogle(HttpContext context)
    {
        var props = new AuthenticationProperties { RedirectUri = "/auth/google/callback" };

        return Results.Challenge(props, new[] { GoogleDefaults.AuthenticationScheme });
    }

    public async Task<IResult> HandleGoogleCallback(HttpContext context)
    {
        var result = await context.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded || result.Principal == null)
            return Results.Unauthorized();

        var claims = result.Principal.Identities.First().Claims;
        var email = claims
            .FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            )
            ?.Value;
        var name = claims
            .FirstOrDefault(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            )
            ?.Value;

        if (email == null)
            return Results.BadRequest(new { message = "❌ Email não encontrado" });

        var user =
            _userService.GetByEmail(email) ?? _userService.CreateFromOAuth(name ?? "", email);

        var token = _userService.GenerateJwt(user);

        await context.SignInAsync("Cookies", result.Principal);

        return Results.Ok(
            new
            {
                message = "✅ Login successful",
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                },
                token,
            }
        );
    }
}
