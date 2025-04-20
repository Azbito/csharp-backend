using backend.DTOs;
using FluentValidation;

public class DeletePostValidator : AbstractValidator<DTODeletePost>
{
    public DeletePostValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
