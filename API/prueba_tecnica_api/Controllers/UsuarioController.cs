using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using prueba_tecnica_api.dominio.Repositories;
using prueba_tecnica_api.DTO;
using prueba_tecnica_api.Extensions;
using prueba_tecnica_api.Responses;

namespace prueba_tecnica_api.Controllers
{
    /// <summary>
    /// Controlador de los usuarios, aquí se realizan las operaciones CRUD sobre los ususarios
    /// </summary>
    [Authorize]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        /// <summary>
        /// Método para listar usuarios en la base de datos
        /// </summary>
        /// <returns>Una lista de todos los usuarios existentes en la base de datos</returns>
        [HttpGet("listar")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var generalResponse = new GeneralResponse<List<UsuarioDTO>>();

            try
            {
                generalResponse.Data = (await _usuarioRepositorio.ListarUsuarios()).ToUsuarioDTOList();
                generalResponse.HttpCode = 200;
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de consultar los usuarios", ex);
            }

            return Ok(generalResponse);
        }

        /// <summary>
        /// Método para agregar un usuario en la base de datos
        /// </summary>
        /// <param name="usuario">DTO del usuario a agregar en la base de datos</param>
        /// <returns>Una respuesta si la operación fue exitosa</returns>
        [HttpPost("agregar")]
        public IActionResult AgregarUsuario([FromBody] UsuarioDTO usuario)
        {
            GeneralResponse<bool> generalResponse = new GeneralResponse<bool>();
            try
            {
                var result = _usuarioRepositorio.Agregar(usuario.ToUsuario());
                generalResponse.HttpCode = 201;
                generalResponse.Data = result;
            }
            catch(SqlException ex)
            {
                string messageError;

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint 'UQ__Usuarios__2CDDD194142D3693'"))
                {
                    messageError = "El campo CURP ya se ha registrado anteriormente";
                }
                else
                {
                    messageError = "Ocurrió un error al tratar de agregar un usuario nuevo";
                }

                generalResponse.SetError(messageError, ex);
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de agregar un usuario nuevo", ex);
            }
            return Ok(generalResponse);
        }

        /// <summary>
        /// Método para modificar un usuario en la base de datos
        /// </summary>
        /// <param name="usuario">DTO del usuario a modificar en base de datos</param>
        /// <returns>Una respuesta si la operación fue exitosa</returns>
        [HttpPut("actualizar")]
        public IActionResult ModificarUsuario([FromBody] UsuarioDTO usuario)
        {
            GeneralResponse<bool> generalResponse = new GeneralResponse<bool>();
            try
            {
                var result = _usuarioRepositorio.Modificar(usuario.ToUsuario());
                generalResponse.HttpCode = 200;
                generalResponse.Data = result;
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de modificar el usuario", ex);
            }
            return Ok(generalResponse);
        }

        /// <summary>
        /// Método para borrar el usuario en la base de datos
        /// </summary>
        /// <param name="usuarioId">Id del usuario para borrar en la base de datos</param>
        /// <returns>Una respuesta si la operación fue exitosa</returns>
        [HttpDelete("borrar/{usuarioId}")]
        public IActionResult BorrarUsuario(int usuarioId)
        {
            GeneralResponse<bool> generalResponse = new GeneralResponse<bool>();
            try
            {
                var result = _usuarioRepositorio.Borrar(usuarioId);
                generalResponse.HttpCode = 204;
                generalResponse.Data = result;
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de borrar al usuario", ex);
            }
            return Ok(generalResponse);
        }
    }
}
