using FluentValidation.Results;

namespace WashingMachineManagementApi.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(fg => fg.Key, fg => fg.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
