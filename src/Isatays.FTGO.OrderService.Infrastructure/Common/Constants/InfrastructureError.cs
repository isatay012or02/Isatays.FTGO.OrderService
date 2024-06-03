using Isatays.FTGO.OrderService.Core.Common.Constants;
using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Infrastructure.Common.Constants;

public static class InfrastructureError
{
    public static Error HttpClientFail => new(ErrorCode.ExternalError, "Error on http client service when the send request");
    public static Error ValidationFail => new(ErrorCode.ExternalError, "Error at validation in a service of order when the send request");
}