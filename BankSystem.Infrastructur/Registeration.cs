using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.Interceptor;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Options;
using BankSystem.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankSystem.Infrastructure
{
    public static class Registeration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //var httpContextAccessor = services.BuildServiceProvider()
            //    .GetRequiredService<IHttpContextAccessor>();

            services.AddPersistence(configuration);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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
        }
    }

    
}
