namespace Isatays.FTGO.OrderService.Core.Payments;

public record ProcessPaymentCommand(Guid OrderId, decimal Amount);