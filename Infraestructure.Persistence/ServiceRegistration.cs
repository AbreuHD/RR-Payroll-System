using Core.Application.Interface.Repositories;
using Core.Application.Interface.Repository;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersistenceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m =>
                    m.MigrationsAssembly(typeof(PersistenceContext).Assembly.FullName)));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IProyectoRepository, ProyectoRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            services.AddTransient<IEmpleadoProyectosRepository, EmpleadoProyectosRepository>();
            services.AddTransient<IPuestoRepository, PuestoRepository>();
            services.AddTransient<INacionalidadRepository, NacionalidadRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();
            services.AddTransient<IAsistenciaRepository, AsistenciaRepository>();
            services.AddTransient<IHorasRepository, HorasRepository>();

            services.AddTransient<ITipoPagoRepository, TipoPagoRepository>();
            services.AddTransient<IPagoRepository, PagoRepository>();
            services.AddTransient<ITipoBancoRepository, TipoBancoRepository>();
            services.AddTransient<ITipoCuentaRepository, TipoCuentaRepository>();
            services.AddTransient<IPercepcionesRepository, PercepcionesRepository>();
            services.AddTransient<IDeduccionesRepository, DeduccionesRepository>();
            services.AddTransient<IActividadesAsignadasRepository, ActividadesAsignadasRepository>();
            services.AddTransient<IActividadesRepository, ActividadesRepository>();            
            services.AddTransient<IPermisoRepository, PermisoRepository>();
            services.AddTransient<IPago_PercepcionesRepository, Pago_PercepcionesRepository>();
            services.AddTransient<IPago_DeduccionesRepository, Pago_DeduccionesRepository>();

        }
    }
}