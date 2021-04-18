using API.Data;
using API.Helpers;
using API.Interface;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<IAddPhotoService, AddPhotoService>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);


            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
            });

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddSignInManager<SignInManager<AppUser>>()
            .AddEntityFrameworkStores<DataContext>();


            services.AddSignalR();

            return services;
        }
    }
}
