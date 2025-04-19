using backend.DTOs;
using FluentValidation;

public class CreatePostValidator : AbstractValidator<DTOCreatePost>
{
    public CreatePostValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
