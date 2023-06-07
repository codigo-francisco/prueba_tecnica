using Microsoft.AspNetCore.Identity;
using prueba_tecnica_api.dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Repositories
{
    /// <summary>
    /// Repositorio para manejo de autenticaciones de Usuarios
    /// </summary>
    public interface IUserAuthenticationRepositorio
    {
        /// <summary>
        /// Método para registrar usuarios asincronicamente
        /// </summary>
        /// <param name="user">Usuario a registrar, de tipo identity</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns>Un resultado identity indicando si fue satisfactorio el registro o si ocurrieron errores</returns>
        Task<IdentityResult> RegisterUserAsync(UserIdentity user, string password);
        /// <summary>
        /// Método para validar si un usuario hace un login autentico
        /// </summary>
        /// <param name="userName">Nombre del usuario a validar</param>
        /// <param name="password">Contraseña del usuario a validar</param>
        /// <returns>El usuario que se ha autenticado correctamente o nulo en caso de una autenticación fallida</returns>
        Task<UserIdentity?> ValidateUser(string userName, string password);
    }
}
