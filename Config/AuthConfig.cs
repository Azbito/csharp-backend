using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace backend.Config
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("GOOGLE_ISSUER"),
                        ValidAudience = Environment.GetEnvironmentVariable("GOOGLE_AUDIENCE"),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                Environment.GetEnvironmentVariable("JWT_KEY") ?? ""
                            )
                        ),
                    };
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/google";
                    options.LogoutPath = "/auth/logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? "";
                    options.ClientSecret =
                        Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? "";
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Scope.Add("email");
                });

            return services;
        }
    }
}
