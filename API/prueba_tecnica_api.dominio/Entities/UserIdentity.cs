using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Entities
{
    /// <summary>
    /// Entidad del usuario para autenticacíón en base de datos (distinto a los usuarios administrados por la entidad Usuario)
    /// </summary>
    public class UserIdentity : IdentityUser
    {
    }
}
