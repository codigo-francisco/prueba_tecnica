using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using prueba_tecnica_api.dominio.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace prueba_tecnica_api.Tokens
{
    /// <summary>
    /// Administrador de Tokens para usuario
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// Variable para obtener las configuraciones de la aplicaciónes (appsettings)
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Variable para administrar usuarios de identity
        /// </summary>
        private readonly UserManager<UserIdentity> _userManager;
        /// <summary>
        /// Constructor del Token Manager
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <param name="userManager">Administrador de usuarios de identity framework</param>
        public TokenManager(
            IConfiguration configuration,
            UserManager<UserIdentity> userManager
        )
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        /// <summary>
        /// Método para crear el token, sirve para gestionar los procesos de creación, primero se crean las credenciales, posteriormente los claims personales 
        /// y finalmente se configuran como opciónes del token a generar
        /// </summary>
        /// <param name="user">Usuario al que se le va a generar el token</param>
        /// <returns>Token generado para el usuario</returns>
        public async Task<string> CreateTokenAsync(UserIdentity user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        /// <summary>
        /// Método para crear las credenciales a partir de la configuración de la aplicación
        /// </summary>
        /// <returns>Las credenciales creadas con la configuración de la aplicación, firmadas con la llave secreta y un algoritmo de cifrado</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("JwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        /// <summary>
        /// Obtiene los claims personales del usuario, agrega el nombre de usuario y los roles en caso de tener alguno
        /// </summary>
        /// <param name="user">Usuario que se utilizara para generar los claims</param>
        /// <returns>Una lista de claims personales del usuario</returns>
        private async Task<List<Claim>> GetClaims(UserIdentity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach ( var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        /// <summary>
        /// Método para generar el objeto de opciones del token
        /// </summary>
        /// <param name="signingCredentials">Credenciales de la aplicación</param>
        /// <param name="claims">Claims del usuario</param>
        /// <returns>La configuración de seguridad del token</returns>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtConfig");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(jwtSettings["expiresIn"])),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
