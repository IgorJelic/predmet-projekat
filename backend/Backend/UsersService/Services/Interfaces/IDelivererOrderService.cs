using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Dto;

namespace UsersService.Services.Interfaces
{
    public interface IDelivererOrderService
    {
        List<OrderInfoDto> GetMyOrders(long delivererId);
        OrderInfoDto GetCurrentOrder(long delivererId);
        List<OrderInfoDto> GetAvailableOrders();
        long ConfirmOrder(long delivererId, long orderId);
    }
}
