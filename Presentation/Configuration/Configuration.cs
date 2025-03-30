using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Services.Concretes;
using UserService.Application.Services.Interfaces;
using UserService.Application.Validators;
using UserService.Domain.DTOs.Permission;
using UserService.Domain.DTOs.Role;
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

            services.AddDbContext<UserServiceDbContext>(
                options => options.UseNpgsql(
                    connection,
                    b => b.MigrationsAssembly(typeof(UserServiceDbContext).Assembly.FullName)
                )
            );

            services.AddScoped<IService<PermissionDTO, int>, PermissionService>();
            services.AddScoped<IRepository<PermissionDTO, int>, PermissionRepository>();
            services.AddScoped<IService<RoleDTO, int>, RoleService>();
            services.AddScoped<IRepository<RoleDTO, int>, RoleRepository>();

            services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleValidator>();

            return services;
        }
    }
}