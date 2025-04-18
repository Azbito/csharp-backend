using backend.Controllers;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Routes;

public static class PostRoutes
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost(
            "/post",
            ([FromServices] PostController controller, [FromBody] DTOCreatePost body) =>
            {
                controller.CreatePost(body);
            }
        );
    }
}
