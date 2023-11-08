namespace Isatays.FTGO.OrderService.Infrastructure.Common.Exceptions;

public class RetryableException : HttpRequestException
{
    public RetryableException(string methodName)
            : base($"Обращение к HTTP методу {methodName} завершилось ошибкой требуется повторная отправка запроса") { }
}
