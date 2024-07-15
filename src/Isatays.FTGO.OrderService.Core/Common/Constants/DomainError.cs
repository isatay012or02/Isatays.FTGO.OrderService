using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.OrderService.Core.Common.Constants;

public static class DomainError
{
    public static Error NotFound => new(ErrorCode.LogicConflict, "Сущность не найдена в базе данных");
    public static Error DatabaseFailed => new(ErrorCode.DatabaseError, "Возникла проблема при обращении к базе данных");
    public static Error SmsFailed => new(ErrorCode.ExternalError, "Возникла проблема при отправке СМС");
    public static Error LogicFailedWithCustomMessage(string message) => new(ErrorCode.LogicConflict, message);
    public static Error DatabaseFailedWithCustomMessage(string message) => new(ErrorCode.DatabaseError, message);
    public static Error ExternalErrorWithCustomMessage(string message) => new(ErrorCode.ExternalError, message);
    public static Error ConvertFailed => new(ErrorCode.ConvertError, "Не удалось десериализовать объект");
}