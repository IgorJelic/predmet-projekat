using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.DbInfrastructure;
using UsersService.Dto;
using UsersService.Models;
using UsersService.Services.Interfaces;

namespace UsersService.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerOrderService(UserDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public OrderInfoDto CreateOrder(long userId, OrderCreateDto order)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            if (user.CustomerOrders == null)
            {
                user.CustomerOrders = new List<Order>();
            }

            Order newOrder = _mapper.Map<Order>(order);

            newOrder.DeliveryTime = DateTime.Now.AddYears(1);
            newOrder.Confirmed = false;
            newOrder.Delivered = false;

            user.CustomerOrders.Add(newOrder);
            _dbContext.SaveChanges();

            OrderInfoDto retVal = _mapper.Map<OrderInfoDto>(user.CustomerOrders.Last());
            return retVal;
        }

        public OrderInfoDto GetCurrentOrder(long userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            var lastOrder = user.CustomerOrders.Last();
            if (lastOrder.DeliveryTime > DateTime.Now)
            {
                return _mapper.Map<OrderInfoDto>(lastOrder);
            }

            return null;
        }

        public List<OrderInfoDto> GetMyOrders(long userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            // Samo zavrsene porudzbine
            var myOrders = user.CustomerOrders.Where(o => o.DeliveryTime < DateTime.Now);

            return _mapper.Map<List<OrderInfoDto>>(myOrders);
        }
    }
}
