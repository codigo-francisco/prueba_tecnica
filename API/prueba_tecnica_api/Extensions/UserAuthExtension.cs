using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.DTO;

namespace prueba_tecnica_api.Extensions
{
    public static class UserAuthExtension
    {
        public static UserIdentity ToUserIdentity(this UserRegistrationDTO userDTO)
        {
            return new UserIdentity()
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber
            };
        }
    }
}
