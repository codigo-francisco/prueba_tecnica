using prueba_tecnica_api.datos.Contexts;
using prueba_tecnica_api.datos.RepositoryImplementations;
using prueba_tecnica_api.dominio.Repositories;

namespace prueba_tecnica_api.Infraestructure
{
    public static class ConfigurationService
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
        {

            var configuration = builder.Configuration;
            var services = builder.Services;

            services.AddSqlServer<GeneralContext>(configuration.GetConnectionString("Default"));
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            return services;
        }
    }
}
