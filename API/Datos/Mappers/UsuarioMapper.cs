using Microsoft.EntityFrameworkCore;
using prueba_tecnica_api.dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.datos.Mappers
{
    internal static class UsuarioMapper
    {
        internal static ModelBuilder ModelingUsuario(this ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Usuario>();

            entity.HasKey(u => u.ID);

            entity.ToTable("Usuarios");

            entity.Property(u => u.ID)
                .HasColumnName("id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(u => u.Nombre)
                .HasColumnName("nombre")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.ApellidoPaterno)
                .HasColumnName("apellidoPaterno")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.ApellidoMaterno)
                .HasColumnName("apellidoMaterno")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Salario)
                .HasColumnName("salario")
                .HasColumnType("money")
                .IsRequired();

            entity.Property(u => u.CURP)
                .HasColumnName("curp")
                .HasColumnType("nvarchar")
                .HasMaxLength(18)
                .IsRequired();

            entity.Property(u => u.Telefono)
                .HasColumnName("telefono")
                .HasColumnType("nvarchar")
                .HasMaxLength(18)
                .IsRequired();

            return modelBuilder;
        }
    }
}
