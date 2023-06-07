using prueba_tecnica_api.dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Repositories
{
    /// <summary>
    /// Interfaz para definir los metodos necesarios de un repositorio de Usuarios
    /// </summary>
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Método para listar todos los usuarios en la base de datos
        /// </summary>
        /// <returns>La lista de todos los usuarios en la base de datos</returns>
        Task<List<Usuario>> ListarUsuarios();
        /// <summary>
        /// Método para agregar usuario nuevo a la base de datos
        /// </summary>
        /// <param name="usuario">Usuario nuevo a agregar a la base de datos</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        bool Agregar(Usuario usuario);
        /// <summary>
        /// Método para modificar un usuario en la base de datos
        /// </summary>
        /// <param name="usuario">Usuario a modificar en la base de datos</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        bool Modificar(Usuario usuario);
        /// <summary>
        /// Método para borrar usuario en la base de datos
        /// </summary>
        /// <param name="id">Id del usuario a eliminar en la base de datos</param>
        /// <returns>Si la operación fue exitosa o no</returns>
        bool Borrar(int id);
    }
}
