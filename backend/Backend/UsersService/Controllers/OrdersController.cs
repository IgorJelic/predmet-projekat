using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersService.Dto;
using UsersService.Services.Interfaces;

namespace UsersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerOrderService _customerOrderService;
        private readonly IDelivererOrderService _delivererOrderService;

        public OrdersController(IOrderService orderService, ICustomerOrderService customerOrderService, IDelivererOrderService delivererOrderService)
        {
            _orderService = orderService;
            _customerOrderService = customerOrderService;
            _delivererOrderService = delivererOrderService;
        }
        

        // ORDER SERVICE
        // GET api/Orders
        [HttpGet]
        //[Authorize(Roles="administrator")
        public IActionResult GetOrders()
        { 
            return Ok(_orderService.GetAllOrders());
        }

        // CUSTOMER/DELIVERER ORDER SERVICE
        // GET api/Orders/MyOrders
        [HttpGet]
        [Route("MyOrders")]
        //[Authorize(Roles="customer,deliverer")
        public IActionResult MyOrders()
        {
            long userId = -1;
            string userRole = "";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
                userRole = identity.FindFirst("role").Value;
            }
            else
            {
                return BadRequest();
            }

            if (userRole.Equals("deliverer"))
            {
                var delivererOrders = _delivererOrderService.GetMyOrders(userId);
                return Ok(delivererOrders);
            }
            else if(userRole.Equals("customer"))
            {
                var customerOrders = _customerOrderService.GetMyOrders(userId);
                return Ok(customerOrders);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/Orders/MyOrders
        [HttpPost]
        [Route("MyOrders")]
        //[Authorize(Roles="customer")
        public IActionResult AddOrder(OrderCreateDto order)
        {
            long userId = -1;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return BadRequest();
            }

            var newOrder = _customerOrderService.CreateOrder(userId, order);
            if (newOrder == null)
            {
                return BadRequest();
            }

            return Ok(newOrder);
        }


        // GET api/Orders/CurrentOrder
        [HttpGet]
        [Route("CurrentOrder")]
        //[Authorize(Roles="customer,deliverer")
        public IActionResult CurrentOrder()
        {
            long userId = -1;
            string userRole = "";

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
                userRole = identity.FindFirst("role").Value;
            }
            else
            {
                return BadRequest();
            }

            if (userRole.Equals("deliverer"))
            {
                var delivererOrders = _delivererOrderService.GetCurrentOrder(userId);
                return Ok(delivererOrders);
            }
            else if (userRole.Equals("customer"))
            {
                var customerOrders = _customerOrderService.GetCurrentOrder(userId);
                return Ok(customerOrders);
            }
            else
            {
                return BadRequest();
            }
        }


        // DELIVERER
        // GET api/Orders/Available
        [HttpGet]
        [Route("Available")]
        //[Authorize(Roles="deliverer")
        public IActionResult AveilableOrders()
        {
            var aveilableOrders = _delivererOrderService.GetAvailableOrders();
            return Ok(aveilableOrders);
        }

        // GET api/Orders/TakeOrder/{orderId}
        [HttpGet]
        [Route("TakeOrder/{orderId}")]
        //[Authorize(Roles="deliverer")
        public IActionResult TakeOrder(long orderId)
        {
            long userId = -1;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return BadRequest();
            }

            var retOrderId = _delivererOrderService.ConfirmOrder(userId, orderId);

            if (retOrderId == -1)
            {
                return BadRequest();
            }

            return Ok(retOrderId);
        }
    }
}
