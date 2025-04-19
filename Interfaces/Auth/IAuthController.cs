using backend.DTOs;

public interface IAuthController
{
    IResult LoginWithGoogle(HttpContext context);
    IResult Login(DTOLogin dto);
    Task<IResult> HandleGoogleCallback(HttpContext context);
}
