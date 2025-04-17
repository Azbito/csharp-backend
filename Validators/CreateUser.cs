using backend.DTOs;
using FluentValidation;

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}
