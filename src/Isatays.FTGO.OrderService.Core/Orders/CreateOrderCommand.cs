using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.OrderService.Core.Orders;

public class CreateOrderCommand : IRequest<Result>
{
    public CreateOrderCommand(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }
}
