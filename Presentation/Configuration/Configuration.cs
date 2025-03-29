using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Context;

namespace UserService.Presentation.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            var connection = Environment.GetEnvironmentVariable("USER_SERVICE_DATABASE_STRING_CONNECTION");

            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentException("Connection not found");
            }

            services.AddDbContext<UserServiceDbContext>(
                options => options.UseNpgsql(
                    connection,
                    b => b.MigrationsAssembly(typeof(UserServiceDbContext).Assembly.FullName)
                )
            );

            return services;
        }
    }
}