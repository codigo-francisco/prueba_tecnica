namespace prueba_tecnica_api.Responses
{
    public class GeneralResponse<T>
    {
        public bool HasError { get; set; } = false;
        public string? MessageError { get; set; }
        public int HttpCode { get; set; } = 200;
        public T? Data { get; set; }

        public void SetError(Exception exception, int errorCode = 500)
        {
            SetError(exception.Message, errorCode);
        }

        public void SetError(string messageError, int errorCode = 500)
        {
            HasError = true;
            HttpCode = errorCode;
            MessageError = messageError;
        }
    }
}
