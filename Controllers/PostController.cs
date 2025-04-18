using backend.DTOs;
using backend.Interfaces;

namespace backend.Controllers;

public class PostController : IPostController
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    public IResult CreatePost(DTOCreatePost dto)
    {
        var validator = new CreatePostValidator();
        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return Results.BadRequest(new { message = "❌ Validation failed", errors });
        }

        var post = _service.Create(dto);

        if (post == null)
        {
            return Results.Conflict("It couldn't create the post, please try again");
        }

        var result = new
        {
            post.Title,
            post.Description,
            post.Attachments,
        };

        return Results.Created("/posts", result);
    }
}
