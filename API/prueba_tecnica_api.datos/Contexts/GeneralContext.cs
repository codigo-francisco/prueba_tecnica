using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using prueba_tecnica_api.datos.Mappers;
using prueba_tecnica_api.dominio.Entities;

#nullable disable

namespace prueba_tecnica_api.datos.Contexts
{
    /// <summary>
    /// Clase que representa el contexto en Base de Datos
    /// </summary>
    public class GeneralContext : IdentityDbContext<UserIdentity>
    {
        /// <summary>
        /// Entidad de Usuarios
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }
        /// <summary>
        /// Constructor default para construir el contexto
        /// </summary>
        /// <param name="options">Opciones para construir el contexto para la clase padre</param>
        public GeneralContext(DbContextOptions options)
            : base(options)
        {
            
        }
        /// <summary>
        /// Método para modelar o crear las entidades
        /// </summary>
        /// <param name="modelBuilder">Objeto que sirve para modelar las entidades</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ModelingUsuario();
        }
    }
}