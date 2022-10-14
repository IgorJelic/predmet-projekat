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
    public class DelivererOrderService : IDelivererOrderService
    {
        private readonly UserDbContext _dbContext;
        private readonly IMapper _mapper;

        public DelivererOrderService(UserDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public long ConfirmOrder(long delivererId, long orderId)
        {
            var deliverer = _dbContext.Users.Find(delivererId);
            var order = _dbContext.Orders.Find(orderId);

            if (deliverer == null || order == null)
            {
                return -1;
            }

            if(deliverer.DelivererOrders == null)
            {
                deliverer.DelivererOrders = new List<Models.Order>();
            }

            Random r = new();
            int deliveryTime = r.Next(5, 5);
            //int deliveryTime = r.Next(1, 1);
            //int deliveryTime = r.Next(10, 10);

            order.DeliveryTime = DateTime.Now.AddMinutes(deliveryTime);
            order.Confirmed = true;

            deliverer.DelivererOrders.Add(order);

            _dbContext.SaveChanges();

            return order.Id;
        }

        public List<OrderInfoDto> GetAvailableOrders()
        {
            var availableOrders = _dbContext.Orders.Include("OrderedProducts")
                .Where(o => o.Confirmed == false).ToList();

            return _mapper.Map<List<OrderInfoDto>>(availableOrders);
        }

        public OrderInfoDto GetCurrentOrder(long delivererId)
        {
            var user = _dbContext.Users.Include("DelivererOrders.OrderedProducts").FirstOrDefault(u => u.Id == delivererId);

            if (user == null)
            {
                return null;
            }

            if (user.DelivererOrders == null)
            {
                user.DelivererOrders = new List<Models.Order>();
            }

            if (user.DelivererOrders.Count > 0)
            {
                var lastOrder = user.DelivererOrders.Last();
                if (lastOrder.DeliveryTime > DateTime.Now)
                {
                    return _mapper.Map<OrderInfoDto>(lastOrder);
                }
            }          

            return new OrderInfoDto() { Id = -1 };
        }

        public List<OrderInfoDto> GetMyOrders(long delivererId)
        {
            //var user = _dbContext.Users.Find(delivererId);
            var user = _dbContext.Users.Include("DelivererOrders.OrderedProducts").FirstOrDefault(u => u.Id == delivererId);

            if (user == null)
            {
                return null;
            }

            if (user.DelivererOrders == null)
            {
                user.DelivererOrders = new List<Models.Order>();
            }

            var myOrders = user.DelivererOrders.Where(o => o.DeliveryTime < DateTime.Now).ToList();

            return _mapper.Map<List<OrderInfoDto>>(myOrders);
        }
    }
}
