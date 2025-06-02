using UserService.Presentation.Responses.Abstracts;

namespace UserService.Presentation.Responses.Concretes
{
    public class SuccessResponse<T>(
        int statusCode,
        string message,
        T data
    ) : Response(statusCode, message)
    {
        public T? Data { get; set; } = data;
    }
}