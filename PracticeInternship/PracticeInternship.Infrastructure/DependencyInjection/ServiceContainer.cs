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
                option.UseSqlServer(config.GetConnectionString("PracticeInternshipConnectionString")),
                ServiceLifetime.Transient,
                ServiceLifetime.Scoped
            );

            // Create dependency injection
            services.AddScoped<Interface_DM_Don_Vi_Tinh, DM_Don_Vi_Tinh_Repository>();
            services.AddScoped<Interface_DM_Loai_San_Pham, DM_Loai_San_Pham_Repository>();
            services.AddScoped<Interface_DM_San_Pham, DM_San_Pham_Repository>();
            services.AddScoped<Interface_DM_NCC, DM_NCC_Repository>();
            services.AddScoped<Interface_DM_Kho, DM_Kho_Repository>();
            services.AddScoped<Interface_DM_Kho_User, DM_Kho_User_Repository>();
            services.AddScoped<Interface_DM_Nhap_Kho, DM_Nhap_Kho_Repository>();
            services.AddScoped<Interface_DM_Nhap_Kho_Raw_Data, DM_Nhap_Kho_Raw_Data_Repository>();
            services.AddScoped<Interface_XNK_Nhap_Kho, XNK_Nhap_Kho_Repository>();
            services.AddScoped<Interface_DM_Xuat_kho, DM_Xuat_Kho_Repository>();

            return services;
        }
    }
}
