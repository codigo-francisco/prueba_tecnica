using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prueba_tecnica_api.dominio.Repositories;
using prueba_tecnica_api.DTO;
using prueba_tecnica_api.Extensions;
using prueba_tecnica_api.Responses;
using prueba_tecnica_api.Tokens;

namespace prueba_tecnica_api.Controllers
{
    /// <summary>
    /// Clase controladora para la autenticación de Usuarios
    /// </summary>
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthenticationRepositorio _userAuthRepositorio;
        private readonly TokenManager _tokenManager;
        public AuthController(
            IUserAuthenticationRepositorio userAuthenticationRepositorio,
            TokenManager tokenManager
        )
        {
            _userAuthRepositorio = userAuthenticationRepositorio;
            _tokenManager = tokenManager;
        }

        /// <summary>
        /// Método asincronico para el registro de usuarios
        /// </summary>
        /// <param name="userRegister">Usuario a registrar</param>
        /// <returns>Una respuesta que indica si la operación fue exitosa o no</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegister)
        {
            var generalResponse = new GeneralResponse<bool>();
            try
            {
                var userResult = await _userAuthRepositorio.RegisterUserAsync(userRegister.ToUserIdentity(), userRegister.Password);

                if (userResult.Errors.Any())
                {
                    generalResponse.HasError = true;

                    var error = userResult.Errors.FirstOrDefault();

                    generalResponse.MessageException = error?.Description;
                    
                    generalResponse.MessageError = "No se pudo registrar al usuario";
                }
                else
                {
                    generalResponse.Data = userResult.Succeeded;
                    generalResponse.HttpCode = 201;
                }
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Ocurrió un error al tratar de registrar al usuario", ex);
            }

            return Ok(generalResponse);
        }

        /// <summary>
        /// Método para realizar el login de un usuario registrado
        /// </summary>
        /// <param name="userLogin">Usuario que va a realizar el login</param>
        /// <returns>Una respuesta que contiene el token generado a partir de la autenticación</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLogin)
        {
            var generalResponse = new GeneralResponse<dynamic>();

            try
            {
                var user = await _userAuthRepositorio.ValidateUser(userLogin.UserName, userLogin.Password);

                if (user != null)
                {
                    var token = await _tokenManager.CreateTokenAsync(user);
                    generalResponse.Data = new {
                        token
                    };
                }
                else
                {
                    generalResponse.HasError = true;
                    generalResponse.MessageException = "";
                    generalResponse.MessageError = "Usuario o contraseña invalidos";
                }
            }
            catch (Exception ex)
            {
                generalResponse.SetError("Error al tratar de realizar el login", ex);
            }

            return Ok(generalResponse);
        }
    }
}
