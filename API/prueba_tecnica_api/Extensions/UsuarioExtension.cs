using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.DTO;

namespace prueba_tecnica_api.Extensions
{
    /// <summary>
    /// Clase que almacena las extensiones de conversión de datos para la entidad de Usuario y UsuarioDTO
    /// </summary>
    public static class UsuarioExtension
    {
        /// <summary>
        /// Tranforma un DTO de Usuario a una entidad de Usuario
        /// </summary>
        /// <param name="dto">DTO del usuario a transformar</param>
        /// <returns>La entidad Usuario</returns>
        public static Usuario ToUsuario(this UsuarioDTO dto)
        {
            return new Usuario
            {
                ApellidoMaterno = dto.ApellidoMaterno,
                ApellidoPaterno = dto.ApellidoPaterno,
                CURP = dto.CURP,
                ID = dto.ID ?? 0,
                Nombre = dto.Nombre,
                Salario = dto.Salario,
                Telefono = dto.Telefono
            };
        }

        /// <summary>
        /// Transforma una entidad de usuario a un DTO de usuario
        /// </summary>
        /// <param name="usuario">Entidad de Usuario a transformar</param>
        /// <returns>El DTO de Usuario a enviar al cliente</returns>
        public static UsuarioDTO ToUsuarioDTO(this Usuario usuario)
        {
            return new UsuarioDTO
            {
                ApellidoMaterno = usuario.ApellidoMaterno,
                ApellidoPaterno = usuario.ApellidoPaterno,
                CURP = usuario.CURP,
                ID = usuario.ID,
                Nombre = usuario.Nombre,
                Salario = usuario.Salario,
                Telefono = usuario.Telefono
            };
        }

        /// <summary>
        /// Transforma una lista de entidades de Usuario a DTO de Usuarios
        /// </summary>
        /// <param name="usuarios">Lista de entidad de Usuarios a transformar</param>
        /// <returns>Lista de DTO de Usuarios a enviar</returns>
        public static List<UsuarioDTO> ToUsuarioDTOList(this List<Usuario> usuarios)
        {
            return usuarios.Select(u => u.ToUsuarioDTO()).ToList();
        }
    }
}
