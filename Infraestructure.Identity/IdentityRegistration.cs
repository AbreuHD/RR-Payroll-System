using Core.Application.Interface.Services;
using Infraestructure.Identity.Services;
using Infrastructure.Identity.Context;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Identity
{
    public static class IdentityRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<IdentityContext>()
               .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/User";
                option.AccessDeniedPath = "/User/AccessDenied";
            }
             );
            services.AddAuthentication();
            services.AddTransient<IAccountService, AccountService>();

        }
    }
}