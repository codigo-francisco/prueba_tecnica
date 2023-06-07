using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_tecnica_api.dominio.Entities
{
    /// <summary>
    /// Entidad de Usuario que representa la tabla en Base de Datos
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// ID del Usuario, es llave primaria
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Apellido Paterno del Usuario
        /// </summary>
        public string ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido Materno del Usuario
        /// </summary>
        public string ApellidoMaterno { get; set; }
        /// <summary>
        /// Salario del Usuario
        /// </summary>
        public decimal Salario { get; set; }
        /// <summary>
        /// CURP del Usuario, es un dato unico
        /// </summary>
        public string CURP { get; set; }
        /// <summary>
        /// Telefono del Usuario
        /// </summary>
        public string Telefono { get; set; }
    }
}
