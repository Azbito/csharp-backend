namespace backend.Interfaces
{
    public interface IAuthService
    {
        IResult LoginWithGoogle(HttpContext context);
        Task<IResult> HandleGoogleCallback(HttpContext context);
    }
}
