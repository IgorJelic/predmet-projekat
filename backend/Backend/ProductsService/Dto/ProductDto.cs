using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        public string Ingredients { get; set; }
    }
}
