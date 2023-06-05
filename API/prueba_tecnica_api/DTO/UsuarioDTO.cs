namespace prueba_tecnica_api.DTO
{
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
