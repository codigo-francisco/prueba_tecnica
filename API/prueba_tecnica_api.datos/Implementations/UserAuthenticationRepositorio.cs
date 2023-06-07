using Microsoft.AspNetCore.Identity;
using prueba_tecnica_api.dominio.Entities;
using prueba_tecnica_api.dominio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.datos.Implementations
{
    public class UserAuthenticationRepositorio : IUserAuthenticationRepositorio
    {
        private UserManager<UserIdentity> _userManager;
        public UserAuthenticationRepositorio(
            UserManager<UserIdentity> userManager
        )
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(UserIdentity user, string password)
        { 
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<UserIdentity?> ValidateUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);

                if (result)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
