using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.Interceptor;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Options;
using BankSystem.Infrastructure.Repository;
using BankSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BankSystem.Infrastructure
{
    public static class Registeration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddPersistence(configuration);
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddConfigurations(configuration);
        }
        private static void AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BankSystem");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.AddInterceptors(new SaveingInterceptor());
                options.UseSqlServer(connectionString);
            });
        }

        private static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BankInfoOption>(configuration.GetSection("BankInfo"));
            services.Configure<UserInfoOption>(configuration.GetSection("UserInfo"));
            var userInfoOptions = services.BuildServiceProvider().GetRequiredService<IOptions<UserInfoOption>>().Value;
            ChangeTrackingService.Configure(new UserInfoOption { UserName = userInfoOptions.UserName });
        }
    }

    
}
