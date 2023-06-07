using Microsoft.AspNetCore.Identity;
using prueba_tecnica_api.dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Repositories
{
    public interface IUserAuthenticationRepositorio
    {
        Task<IdentityResult> RegisterUserAsync(UserIdentity user, string password);
        Task<UserIdentity?> ValidateUser(string userName, string password);
    }
}
