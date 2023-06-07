namespace prueba_tecnica_api.Responses
{
    /// <summary>
    /// Clase usada para envolver una respuesta general al cliente
    /// </summary>
    /// <typeparam name="T">Parametro que inidca el tipo de datos en la respuesta</typeparam>
    public class GeneralResponse<T>
    {
        /// <summary>
        /// Indica si la respuesta tiene o no un error
        /// </summary>
        public bool HasError { get; set; } = false;
        /// <summary>
        /// Mensaje de error para el cliente
        /// </summary>
        public string? MessageError { get; set; }
        /// <summary>
        /// Mensaje de la excepcion generada
        /// </summary>
        public string? MessageException { get; set; }
        /// <summary>
        /// Codigo HTTP generada
        /// </summary>
        public int HttpCode { get; set; } = 200;
        /// <summary>
        /// Datos de respuesta
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Método para programar una respuesta generada con una excepción
        /// </summary>
        /// <param name="messageError">Mensaje de error que se mostrara para el cliente</param>
        /// <param name="exception">Excepción generada</param>
        /// <param name="errorCode">Codigo de error</param>
        public void SetError(string messageError, Exception exception, int errorCode = 500)
        {
            HasError = true;
            HttpCode = errorCode;
            MessageError = messageError;
            MessageException = exception.Message;
        }
    }
}
