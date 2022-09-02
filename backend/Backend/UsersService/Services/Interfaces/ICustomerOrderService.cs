using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Dto;

namespace UsersService.Services.Interfaces
{
    public interface ICustomerOrderService
    {
        List<OrderInfoDto> GetMyOrders(long userId);
        OrderInfoDto CreateOrder(long userId, OrderCreateDto order);
        OrderInfoDto GetCurrentOrder(long userId);
    }
}
