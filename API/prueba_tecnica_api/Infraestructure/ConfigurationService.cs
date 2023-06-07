using prueba_tecnica_api.datos.Contexts;
using prueba_tecnica_api.datos.RepositoryImplementations;
using prueba_tecnica_api.dominio.Repositories;

namespace prueba_tecnica_api.Infraestructure
{
    public static class ConfigurationService
    {
        /// <summary>
        /// Método para configuración personalizadas de servicios de la aplicación
        /// </summary>
        /// <param name="builder">El webapplication builder que se está utilizando para construir la aplicación</param>
        /// <returns>El builder que se está utilizando en la aplicación</returns>
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {

            var configuration = builder.Configuration;
            var services = builder.Services;

            services.AddCors(options =>
            {
                options.AddPolicy("CorsLocalhost", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSqlServer<GeneralContext>(configuration.GetConnectionString("Default"));
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            return builder;
        }
    }
}
