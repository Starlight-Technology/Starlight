using Starlight.Models;

using System.Net;

namespace Starlight.Exception;

public class ExceptionHandler(ILogRepository logRepository)
{
    private readonly bool _isDebugMode =
#if DEBUG
        true;

#else
        false;
#endif

    private void LogException(string exception) => logRepository.SaveLogAsync(new Log(0, Severity.Error, exception)).ConfigureAwait(true);

    public DefaultResponse HandleException(System.Exception exception)
    {
        string error = $@"Source: {exception.Source};Message: {exception.Message}; StackTrace: {exception.StackTrace}";

        // Log the exception (e.g., to a file or logging service)
        LogException(error);

        // Return a friendly message to the user unless are in DEBUG
        return _isDebugMode
            ? new DefaultResponse(httpStatus: HttpStatusCode.InternalServerError, message: error)
            : new DefaultResponse(
                httpStatus: HttpStatusCode.InternalServerError,
                message: "An error has occurred. Don't be afraid! An email with the error details has been sent to your developers.");
    }
}
