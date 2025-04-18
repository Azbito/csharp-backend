using backend.Controllers;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this WebApplication app)
    {
        app.MapPost(
            "/users",
            ([FromServices] UserController controller, [FromBody] CreateUser dto) =>
                controller.CreateUser(dto)
        );

        app.MapGet(
            "/users",
            ([FromServices] UserController controller) => controller.GetAllUsers()
        );

        app.MapGet(
            "/users/id/{id}",
            ([FromServices] UserController controller, string id) => controller.GetUserById(id)
        );

        app.MapGet(
            "/users/{username}",
            ([FromServices] UserController controller, string username) =>
                controller.GetUserByUsername(username)
        );
    }
}
