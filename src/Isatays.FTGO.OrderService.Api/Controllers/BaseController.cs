using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using KDS.Primitives.FluentResult;
using Isatays.FTGO.OrderService.Core.Common.Constants;

namespace Isatays.FTGO.OrderService.Api.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(statusCode: (int)HttpStatusCode.ServiceUnavailable, type: typeof(ProblemDetails))]
[ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(ProblemDetails))]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Получает доступ к интерфейсу медиатора
    /// </summary>
    protected ISender Sender =>
        HttpContext.RequestServices.GetRequiredService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));

    /// <summary>
    /// Получает доступ к интерфейсу авто маппера
    /// </summary>
    protected IMapper Mapper =>
        HttpContext.RequestServices.GetRequiredService<IMapper>() ?? throw new ArgumentNullException(nameof(IMapper));

    /// <summary>
    /// Возвращает обработанную ошибку и подбирает статус код в зависимости от полученного кода ошибки
    /// </summary>
    protected ObjectResult ProblemResponse(Error error)
    {
        return error.Code switch
        {
            ErrorCode.DatabaseError => Problem(title: "Ошибка во время подключения к базе данных",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.InternalServerError),
            ErrorCode.ExternalError => Problem(title: "Ошибка во время подключения к сервисной шине",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.ServiceUnavailable),
            ErrorCode.LogicConflict => Problem(title: "Конфликт логической зависимости",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.BadRequest),
            ErrorCode.ParameterError => Problem(title: "Невалидный параметр",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.BadRequest),
            ErrorCode.NotFound => Problem(title: "Не удалось найти информацию",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.NotFound),
            _ => Problem(title: "Необработанное исключение",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.InternalServerError),
        };
    }
}
