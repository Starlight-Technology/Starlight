using Starlight.Models;

namespace Starlight.Exception;

public interface ILogRepository
{
    Task SaveLogAsync(Log log, CancellationToken cancellationToken = default);
}
