using Microsoft.EntityFrameworkCore;
using prueba_tecnica_api.datos.Contexts;
using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.datos.RepositoryImplementations
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GeneralContext _context;
        public UsuarioRepositorio(GeneralContext context) 
        {
            _context = context;
        }
        public bool Agregar(Usuario usuario)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated(
                $"EXEC AltaUsuario {usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Salario},{usuario.CURP},{usuario.Telefono}");

            return affectedRows > 0;
        }
        public bool Modificar(Usuario usuario)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated(
                $"EXEC ActualizarUsuario {usuario.ID},{usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Salario},{usuario.CURP},{usuario.Telefono}");

            return affectedRows > 0;
        }

        public bool Borrar(int id)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated($"EXEC BorrarUsuario {id}");

            return affectedRows > 0;
        }
        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.FromSqlInterpolated($"EXEC ConsultarUsuarios").ToListAsync();
        }
    }
}
