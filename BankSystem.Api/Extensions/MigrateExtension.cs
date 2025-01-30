using BankSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope(); 
            var serviceProvider = scope.ServiceProvider; 
            var dbContext = serviceProvider.GetRequiredService<AppDbContext>(); 
            dbContext.Database.Migrate();
        }
    }
}
