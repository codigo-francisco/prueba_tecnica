using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.DTO;

namespace prueba_tecnica_api.Extensions
{
    /// <summary>
    /// Clase que almacena las extensiones para los DTO y Usuarios de indentityFramework
    /// </summary>
    public static class UserAuthExtension
    {
        /// <summary>
        /// Método para generar un usuario identity a partir de un DTO de registro
        /// </summary>
        /// <param name="userDTO">DTO de registro del usuario</param>
        /// <returns>Un usuario identity que contiene el nombre de usuario y el email</returns>
        public static UserIdentity ToUserIdentity(this UserRegistrationDTO userDTO)
        {
            return new UserIdentity()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };
        }
    }
}
