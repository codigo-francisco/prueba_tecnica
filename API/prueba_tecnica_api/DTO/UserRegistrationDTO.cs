using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_api.DTO
{
    public class UserRegistrationDTO
    {
        public string UserName { get;set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
