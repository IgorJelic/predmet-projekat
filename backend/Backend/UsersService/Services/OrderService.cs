using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.DbInfrastructure;
using UsersService.Dto;
using UsersService.Services.Interfaces;

namespace UsersService.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(UserDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<OrderInfoDto> GetAllOrders()
        {
            var allOrders = _mapper.Map<List<OrderInfoDto>>(_dbContext.Orders.Include("OrderedProducts"));

            allOrders.ForEach(o =>
            {
                if (o.DeliveryTime < DateTime.Now)
                {
                    o.Delivered = true;
                }
            });

            return allOrders;
        }
    }
}
