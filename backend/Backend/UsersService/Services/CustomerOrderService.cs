using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            //var user = _dbContext.Users.Find(userId);
            var user = _dbContext.Users.Include("CustomerOrders.OrderedProducts").FirstOrDefault(u => u.Id == userId);

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
            var user = _dbContext.Users.Include("CustomerOrders.OrderedProducts").FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            if (user.CustomerOrders == null)
            {
                user.CustomerOrders = new List<Models.Order>();
            }

            if (user.CustomerOrders.Count > 0)
            {
                var lastOrder = user.CustomerOrders.Last();
                if (lastOrder.DeliveryTime > DateTime.Now)
                {
                    return _mapper.Map<OrderInfoDto>(lastOrder);
                }
            }           

            return new OrderInfoDto() { Id = -1 };
        }

        public List<OrderInfoDto> GetMyOrders(long userId)
        {
            //var user = _dbContext.Users.Find(userId);
            var user = _dbContext.Users.Include("CustomerOrders.OrderedProducts").FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            if (user.CustomerOrders == null)
            {
                user.CustomerOrders = new List<Order>();
            }

            // Samo zavrsene porudzbine
            var myOrders = user.CustomerOrders.Where(o => o.DeliveryTime < DateTime.Now).ToList();

            return _mapper.Map<List<OrderInfoDto>>(myOrders);
        }
    }
}
