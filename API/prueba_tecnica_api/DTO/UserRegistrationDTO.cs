using System.ComponentModel.DataAnnotations;

#nullable disable

namespace prueba_tecnica_api.DTO
{
    /// <summary>
    /// DTO que almacena los datos del usuario para registrarse en la autenticación
    /// </summary>
    public class UserRegistrationDTO
    {
        public string UserName { get;set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
