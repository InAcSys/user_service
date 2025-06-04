namespace UserService.Presentation.Responses.Abstracts
{
    public abstract class Response(int statusCode, string message)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
    }
}
