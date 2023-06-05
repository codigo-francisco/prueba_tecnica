using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.DTO;

namespace prueba_tecnica_api.Extensions
{
    public static class UsuarioExtension
    {
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

        public static List<UsuarioDTO> ToUsuarioDTOList(this List<Usuario> usuarios)
        {
            return usuarios.Select(u => u.ToUsuarioDTO()).ToList();
        }
    }
}
