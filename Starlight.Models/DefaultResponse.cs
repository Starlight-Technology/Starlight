using System.Net;

namespace Starlight.Models;

public class DefaultResponse(
    object? objectResponse = null,
    HttpStatusCode httpStatus = HttpStatusCode.OK,
    string message = "")
{
    public HttpStatusCode HttpStatus { get; set; } = httpStatus;

    public string Message { get; set; } = message;

    public object? ObjectResponse { get; set; } = objectResponse;
}
