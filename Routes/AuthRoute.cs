using backend.Controllers;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Routes;

public static class AuthRoutes
{
    public static void MapAuthRoutes(this WebApplication app)
    {
        app.MapGet(
            "/auth/google",
            ([FromServices] AuthController controller, HttpContext context) =>
                controller.LoginWithGoogle(context)
        );

        app.MapGet(
            "/auth/google/callback",
            async ([FromServices] AuthController controller, HttpContext context) =>
                await controller.HandleGoogleCallback(context)
        );

        app.MapPost(
            "/auth",
            ([FromServices] AuthController controller, [FromBody] DTOLogin dto) =>
                controller.Login(dto)
        );
    }
}
