using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetUsuarios()
        {
            var generalResponse = new GeneralResponse<List<UsuarioDTO>>();

            try
            {
                generalResponse.Data = _usuarioRepositorio.GetUsuarios().ToUsuarioDTOList();
                generalResponse.HttpCode = 200;
            }
            catch (Exception ex)
            {
                generalResponse.SetError(ex);
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
            catch (Exception ex)
            {
                generalResponse.SetError(ex);
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
                generalResponse.SetError(ex);
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
                generalResponse.SetError(ex);
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
