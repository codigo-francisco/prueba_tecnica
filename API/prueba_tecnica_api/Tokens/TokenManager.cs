using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using prueba_tecnica_api.dominio.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace prueba_tecnica_api.Tokens
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserIdentity> _userManager;
        public TokenManager(
            IConfiguration configuration,
            UserManager<UserIdentity> userManager
        )
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(UserIdentity user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var jwtConfig = _configuration.GetSection("JwtConfig");
            var key = Encoding.UTF8.GetBytes(jwtConfig["secret"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

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
