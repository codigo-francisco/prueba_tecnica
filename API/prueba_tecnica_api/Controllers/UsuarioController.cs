using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using prueba_tecnica_api.dominio.Repositories;
using prueba_tecnica_api.DTO;
using prueba_tecnica_api.Extensions;
using prueba_tecnica_api.Responses;

namespace prueba_tecnica_api.Controllers
{
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetUsuarios()
        {
            var generalResponse = new GeneralResponse<List<UsuarioDTO>>();

            try
            {
                generalResponse.Data = (await _usuarioRepositorio.GetUsuarios()).ToUsuarioDTOList();
                generalResponse.HttpCode = 200;
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de consultar los usuarios", ex);
            }

            return Ok(generalResponse);
        }

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

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Server OK");
        }
    }
}
