using backend.Controllers;
using backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Routes;

public static class PostRoutes
{
    public static void MapPostRoutes(this WebApplication app)
    {
        app.MapPost(
                "/publish",
                (
                    HttpContext http,
                    [FromServices] PostController controller,
                    [FromBody] DTOCreatePost body
                ) =>
                {
                    var userIdClaim =
                        http.User.FindFirst("sub")?.Value ?? http.User.FindFirst("id")?.Value;

                    if (userIdClaim == null)
                    {
                        return Results.Unauthorized();
                    }

                    var authorId = userIdClaim;

                    return controller.CreatePost(body, authorId);
                }
            )
            .RequireAuthorization();
    }
}
