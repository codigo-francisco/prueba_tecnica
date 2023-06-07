using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using prueba_tecnica_api.datos.Contexts;
using prueba_tecnica_api.datos.Implementations;
using prueba_tecnica_api.datos.RepositoryImplementations;
using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.dominio.Repositories;
using prueba_tecnica_api.Tokens;
using System.Text;

namespace prueba_tecnica_api.Infraestructure
{
    /// <summary>
    /// Clase que almacena los métodos de extensión de la configuracion de servicios para la aplicación
    /// </summary>
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

            services.AddSqlServer<GeneralContext>(configuration.GetConnectionString("Default"));

            services.AddIdentity<UserIdentity, IdentityRole>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<GeneralContext>()
            .AddDefaultTokenProviders();

            var jwtConfig = configuration.GetSection("JwtConfig");
            var secretKey = jwtConfig["secret"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["validIssuer"],
                    ValidAudience = jwtConfig["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Web Usuarios",
                    Version = "v1",
                    Description = "API Web para Usuarios en Prueba Tecnica CRUD",
                    Contact = new OpenApiContact
                    {
                        Email = "fcoglezh88@gmail.com",
                        Name = "Francisco Gonzalez Hernandez"
                    }
                });

                c.ResolveConflictingActions(apiDescription => apiDescription.First());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autorizacion basada en el esquema de cabecero bearer JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsLocalhost", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUserAuthenticationRepositorio, UserAuthenticationRepositorio>();
            services.AddScoped<TokenManager>();

            return builder;
        }
    }
}
