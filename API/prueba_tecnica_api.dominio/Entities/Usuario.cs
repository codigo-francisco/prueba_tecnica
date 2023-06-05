using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Entities
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public decimal Salario { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
    }
}
