using System;
using System.Collections.Generic;

namespace UsersService.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string OrderAddress { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
        public bool Confirmed { get; set; }
        public bool Delivered { get; set; } // setujem je prilikom getovanja tako sto proverim trenutno vreme i vreme dostave, a sluzi mi kod adminskog pregleda svih porudzbina, za ustanovu statusa porudzbine
        public DateTime DeliveryTime { get; set; }

        // korisnik koji je porucio porudzbinu
        public long CustomerId { get; set; }
        public User Customer { get; set; }
        // dostavljac koji je prihvatio i dostavlja porudzbinu
        public long? DelivererId { get; set; }
        public User Deliverer { get; set; }
        // lista porucenih proizvoda
        public List<OrderedProduct> OrderedProducts { get; set; }
    }
}