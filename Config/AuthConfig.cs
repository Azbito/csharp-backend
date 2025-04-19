using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationConfig
{
    private static void IntegrateAuthentication(this IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        });

        IntegrateJWT(authenticationBuilder);

        IntegrateCookies(authenticationBuilder);

        IntegrateGoogle(authenticationBuilder);
    }

    private static void IntegrateJWT(
        Microsoft.AspNetCore.Authentication.AuthenticationBuilder authenticationBuilder
    )
    {
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? "")
                ),
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    var userClaims = context.Principal?.Claims;

                    return Task.CompletedTask;
                },
            };
        });
    }

    public static void IntegrateCookies(
        Microsoft.AspNetCore.Authentication.AuthenticationBuilder authenticationBuilder
    )
    {
        authenticationBuilder.AddCookie(options =>
        {
            options.LoginPath = "/auth/google";
            options.LogoutPath = "/auth/logout";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });
    }

    private static void IntegrateGoogle(
        Microsoft.AspNetCore.Authentication.AuthenticationBuilder authenticationBuilder
    )
    {
        authenticationBuilder.AddGoogle(options =>
        {
            options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? "";
            options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? "";
            options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.Scope.Add("email");
        });
    }

    private static void IntegrateAuthPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();
        });
    }

    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
    {
        services.IntegrateAuthentication();
        services.IntegrateAuthPolicy();

        return services;
    }
}
