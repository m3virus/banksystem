using BankSystem.Api.Auth;
using BankSystem.Api.Models;
using Microsoft.AspNetCore.Authentication;

namespace BankSystem.Api
{
    public static class Registration
    {
        public static void AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        }
    }
}
