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
    /// <summary>
    /// Implementación del repositorio de Usuarios
    /// </summary>
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly GeneralContext _context;
        public UsuarioRepositorio(GeneralContext context) 
        {
            _context = context;
        }
        /// <summary>
        /// Agrega a un Usuario nuevo utilizando el SP AgregarUsuario
        /// </summary>
        /// <param name="usuario">Usuario a agregar</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        public bool Agregar(Usuario usuario)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated(
                $"EXEC AgregarUsuario {usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Salario},{usuario.CURP},{usuario.Telefono}");

            return affectedRows > 0;
        }
        /// <summary>
        /// Modifica a un Usuario usando el SP ModificarUsuario
        /// </summary>
        /// <param name="usuario">El Usuario a modificar</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        public bool Modificar(Usuario usuario)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated(
                $"EXEC ModificarUsuario {usuario.ID},{usuario.Nombre},{usuario.ApellidoPaterno},{usuario.ApellidoMaterno},{usuario.Salario},{usuario.CURP},{usuario.Telefono}");

            return affectedRows > 0;
        }
        /// <summary>
        /// Borra a un Usuario usando el SP BorrarUsuario
        /// </summary>
        /// <param name="id">Id de l Usuario a borrar</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        public bool Borrar(int id)
        {
            int affectedRows = _context.Database.ExecuteSqlInterpolated($"EXEC BorrarUsuario {id}");

            return affectedRows > 0;
        }
        /// <summary>
        /// Lista a todos los Usuario de la base de datos
        /// </summary>
        /// <returns>Lista de todos los Usuarios en base de datos</returns>
        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await _context.Usuarios.FromSqlInterpolated($"EXEC ListarUsuarios").ToListAsync();
        }
    }
}
