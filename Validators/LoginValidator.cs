using backend.DTOs;
using FluentValidation;

public class LoginValidator : AbstractValidator<DTOLogin>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
