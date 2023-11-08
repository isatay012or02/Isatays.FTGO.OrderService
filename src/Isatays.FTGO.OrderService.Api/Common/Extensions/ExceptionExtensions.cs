using Isatays.FTGO.OrderService.Core.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Isatays.FTGO.OrderService.Api.Common.Extensions;

public static class ExceptionExtensions
{
    /// <summary>Создает экземпляр класса <see cref="ProblemDetails"/></summary>
	public static ProblemDetails GenerateProblemDetails(this Exception ex,
        HttpContext context,
        string title,
        HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/problem+json";

        ProblemDetails problemDetails = new() { Status = (int)code, Title = title, Detail = ex.Message, };

        if (ex is RequestValidationException requestValidationException)
            problemDetails.Extensions.Add("errors", requestValidationException.Errors.SelectMany(x => x.Value));

        problemDetails.Extensions.Add("correlationId", context.Request.Headers["CorrelationId"].ToString());

        return problemDetails;
    }
}
