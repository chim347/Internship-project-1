using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Infrastructure.Data;
using PracticeInternship.Infrastructure.Repositories;

namespace PracticeInternship.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // add database connectivity
            services.AddDbContext<PracticeInternshipDbContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("PracticeInternshipConnectionString"));
            });

            // Create dependency injection
            services.AddScoped<Interface_DM_Don_Vi_Tinh, DM_Don_Vi_Tinh_Repository>();


            return services;
        }
    }
}
