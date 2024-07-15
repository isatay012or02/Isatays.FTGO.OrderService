using AutoMapper;
using Isatays.FTGO.OrderService.Api.Models;
using Isatays.FTGO.OrderService.Core.Orders;

namespace Isatays.FTGO.OrderService.Api.Common.Profiles;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderCommand>()
            .ReverseMap();
    }
}