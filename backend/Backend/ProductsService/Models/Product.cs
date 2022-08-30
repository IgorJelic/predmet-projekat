using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        public string Ingredients { get; set; }
    }
}
