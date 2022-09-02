using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Dto
{
    public class OrderedProductDto
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public long ProductId { get; set; }
    }
}
