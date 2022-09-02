using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Dto
{
    public class OrderCreateDto
    {
        public long Id { get; set; }
        [Required]
        public string OrderAddress { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public List<OrderedProductDto> OrderedProducts { get; set; }
    }
}
