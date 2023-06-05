using prueba_tecnica_api.dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Repositories
{
    public interface IUsuarioRepositorio
    {
        List<Usuario> GetUsuarios();
        bool Agregar(Usuario usuario);
        bool Modificar(Usuario usuario);
        bool Borrar(int id);
    }
}
