using backend.Interfaces;

namespace backend.Controllers;

public class AuthController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    public IResult LoginWithGoogle(HttpContext context)
    {
        return _authService.LoginWithGoogle(context);
    }

    public Task<IResult> HandleGoogleCallback(HttpContext context)
    {
        return _authService.HandleGoogleCallback(context);
    }
}
