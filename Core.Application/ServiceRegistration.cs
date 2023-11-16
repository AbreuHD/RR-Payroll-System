using Core.Application.Interface.Services;
using Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient<IUserService, UserService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}