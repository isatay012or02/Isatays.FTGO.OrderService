namespace Isatays.FTGO.OrderService.Core.DTO;

public record AuthorizeCreditCardDto(string CreditNumber, DateTime ExpirationDate, int CardCode);
