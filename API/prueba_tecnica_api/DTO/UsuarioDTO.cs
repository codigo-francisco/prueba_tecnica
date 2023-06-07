using System.ComponentModel.DataAnnotations;

namespace prueba_tecnica_api.DTO
{
    /// <summary>
    /// Clase DTO (Data Transfer Object) para enviar y recibir datos del Usuario en API
    /// </summary>
    public class UsuarioDTO
    {
        public int? ID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public decimal Salario { get; set; }
        public string CURP { get; set; }
        public string Telefono { get; set; }
    }
}
