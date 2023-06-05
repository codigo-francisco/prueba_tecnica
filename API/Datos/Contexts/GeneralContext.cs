using Microsoft.EntityFrameworkCore;
using prueba_tecnica_api.datos.Mappers;
using prueba_tecnica_api.dominio.Entities;

namespace prueba_tecnica_api.datos.Contexts
{
    public class GeneralContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public GeneralContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ModelingUsuario();
            base.OnModelCreating(modelBuilder);
        }
    }
}