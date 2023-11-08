using FluentValidation;
using FluentValidation.Results;

namespace Isatays.FTGO.OrderService.Core.Common.Exceptions;

public class RequestValidationException : ValidationException
{
    public RequestValidationException() : base("Ошибка во время валидации параметров запроса: ")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public RequestValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public new IDictionary<string, string[]> Errors { get; }
}
