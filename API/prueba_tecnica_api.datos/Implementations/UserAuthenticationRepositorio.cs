using Microsoft.AspNetCore.Identity;
using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.datos.Implementations
{
    /// <summary>
    /// Implementación del repositorio de autenticación de usuarios
    /// </summary>
    public class UserAuthenticationRepositorio : IUserAuthenticationRepositorio
    {
        /// <summary>
        /// Variable para manejar la administración de usuarios de identity framework
        /// </summary>
        private readonly UserManager<UserIdentity> _userManager;
        /// <summary>
        /// Constructor del repositorio para autenticar usuarios
        /// </summary>
        /// <param name="userManager">Administrador de usuarios de identity framework</param>
        public UserAuthenticationRepositorio(
            UserManager<UserIdentity> userManager
        )
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Método para registrar usuarios de manera asincronica
        /// </summary>
        /// <param name="user">Usuario de tipo identity a registrar</param>
        /// <param name="password">Contraseña del usuario a registrar</param>
        /// <returns>IdentityResult que establece si la operación fue exitosa o presentó errores</returns>
        public async Task<IdentityResult> RegisterUserAsync(UserIdentity user, string password)
        { 
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        /// <summary>
        /// Método para validar si un nombre de usuario y contraseña son validos
        /// </summary>
        /// <param name="userName">Nombre de usuario a validar</param>
        /// <param name="password">Contraseña del usuario a validar</param>
        /// <returns>En caso de ser correcta la validación devuelve un usuario identity, caso contrase devuelve null</returns>
        public async Task<UserIdentity?> ValidateUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                if (result)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
