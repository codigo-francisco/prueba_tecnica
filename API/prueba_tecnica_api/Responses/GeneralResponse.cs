namespace prueba_tecnica_api.Responses
{
    public class GeneralResponse<T>
    {
        public bool HasError { get; set; } = false;
        public string? MessageError { get; set; }
        public string? MessageException { get; set; }
        public int HttpCode { get; set; } = 200;
        public T? Data { get; set; }

        public void SetError(string messageError, Exception exception, int errorCode = 500)
        {
            HasError = true;
            HttpCode = errorCode;
            MessageError = messageError;
            MessageException = exception.Message;
        }
    }
}
