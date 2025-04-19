using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class PostController : ControllerBase, IPostController
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    public IResult CreatePost(DTOCreatePost dto, string authorId)
    {
        var validator = new CreatePostValidator();
        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return Results.BadRequest(new { message = "❌ Validation failed", errors });
        }

        var post = _service.Create(dto, authorId);

        if (post == null)
        {
            return Results.Conflict("It couldn't create the post. Are you logged in?");
        }

        var result = new
        {
            post.Title,
            post.Description,
            post.Attachments,
        };

        return Results.Created("/publish", result);
    }
}
