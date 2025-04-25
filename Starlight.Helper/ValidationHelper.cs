using FluentValidation.Results;

namespace Starlight.Helper;

public static class ValidationHelper
{
    public static string GetValidationErrorsMessage(this IEnumerable<ValidationFailure> failures)
    {
        return string.Join("; ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));
    }

    public static string GetValidationErrorsMessage(this ValidationResult result)
    {
        return string.Join("; ", result.Errors.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));
    }
}
