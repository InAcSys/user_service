using UserService.Presentation.Responses.Abstracts;

namespace UserService.Presentation.Responses.Concretes
{
    public class ErrorResponse(
        int statusCode,
        string message,
        List<string>? errors
    ) : Response(statusCode, message)
    {
        public List<string> Errors { get; set; } = errors ?? [];
    }
}