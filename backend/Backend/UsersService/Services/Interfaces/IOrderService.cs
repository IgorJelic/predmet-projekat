using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Dto;

namespace UsersService.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderInfoDto> GetAllOrders();
        
    }
}
