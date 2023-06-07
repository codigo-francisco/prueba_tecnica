using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using prueba_tecnica_api.datos.Mappers;
using prueba_tecnica_api.dominio.Entities;

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
        public GeneralContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ModelingUsuario();
        }
    }
}