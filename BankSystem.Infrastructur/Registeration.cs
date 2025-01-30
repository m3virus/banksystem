﻿using BankSystem.Infrastructure.Context;
using BankSystem.Infrastructure.Interceptor;
using BankSystem.Infrastructure.IRepository;
using BankSystem.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankSystem.Infrastructure
{
    public static class Registeration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
        private static void AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.AddInterceptors(new SaveingInterceptor());
                options.UseSqlServer(configuration.GetValue<string>("AppDb:ConnectionString"));
            });
        }
    }

    
}
