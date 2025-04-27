// using Isatays.FTGO.OrderService.Core.Common.Constants;
// using Isatays.FTGO.OrderService.Core.Entities;
// using Isatays.FTGO.OrderService.Core.Ports;
// using KDS.Primitives.FluentResult;
// using MassTransit;
// using Microsoft.Extensions.Logging;
//
// namespace Isatays.FTGO.OrderService.Core.Orders;
//
// public class CreateOrderCommandHandler(IOrderService service, 
//     IDataContext dataContext, 
//     ILogger<CreateOrderCommandHandler> logger) : IConsumer<CreateOrderCommand>
// {
//     public async Task Consume(ConsumeContext<CreateOrderCommand> context)
//     {
//         await using (var transaction = await dataContext.Database.BeginTransactionAsync())
//         {
//             try
//             {
//                 var command = context.Message;
//
//                 var order = new Order
//                 {
//                     Id = command.OrderId,
//                     CustomerId = command.CustomerId
//                 };
//
//                 await dataContext.Orders.AddAsync(order);
// 				
//                 await Task.WhenAll(
//                     dataContext.SaveChangesAsync(),
//                     service.CreateOrder(order),
//                     context.Publish(order)
//                     );
// 				
//                 await transaction.CommitAsync();
//             }
//             catch (Exception ex)
//             {
//                 await transaction.RollbackAsync();
//                 logger.LogError("Exception message: {Message}", ex.Message);
//                 Result.Failure(DomainError.DatabaseFailed);
//             }
//         }
//
//         Result.Success();
//     }
// }