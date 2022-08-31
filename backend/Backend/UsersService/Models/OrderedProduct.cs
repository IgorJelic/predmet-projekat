using System.ComponentModel.DataAnnotations;

namespace UsersService.Models
{
    public class OrderedProduct
    {
        public long Id { get; set; }
        [Range(1, double.MaxValue)]
        public double Amount { get; set; }

        // proizvod koji se narucuje
        public long ProductId { get; set; }

        // porudzbina kojoj pripada poruceni proizvod
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}