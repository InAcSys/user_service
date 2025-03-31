using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

using UserService.Application.Services.Concretes;
using UserService.Application.Services.Interfaces;
using UserService.Application.Validators;
using UserService.Domain.Entities.Concretes;
using UserService.Infrastructure.Context;
using UserService.Infrastructure.Repositories.Concretes;
using UserService.Infrastructure.Repositories.Interfaces;

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

            Console.WriteLine("==============================================================");
            Console.WriteLine("Connection String:");
            Console.WriteLine(connection);
            Console.WriteLine("==============================================================");

            services.AddDbContext<DbContext, UserServiceDbContext>(
                options => options.UseNpgsql(
                    connection,
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)
                )
            );

            services.AddScoped<IService<Permission, int>, PermissionService>();
            services.AddScoped<IRepository<Permission, int>, PermissionRepository>();
            services.AddScoped<IService<Role, int>, RoleService>();
            services.AddScoped<IRepository<Role, int>, RoleRepository>();
            services.AddScoped<IService<RolePermission, int>, RolePermissionService>();
            services.AddScoped<IRepository<RolePermission, int>, RolePermissionRepository>();

            services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            services.AddValidatorsFromAssemblyContaining<RolePermissionValidator>();

            return services;
        }
    }
}