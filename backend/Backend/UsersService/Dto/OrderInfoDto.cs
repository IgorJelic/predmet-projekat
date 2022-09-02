using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Dto
{
    public class OrderInfoDto
    {
        public long Id { get; set; }
        public string OrderAddress { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
        public bool Confirmed { get; set; }
        public bool Delivered { get; set; }
        public DateTime DeliveryTime { get; set; }
        public List<OrderedProductDto> OrderedProducts { get; set; }
    }
}
