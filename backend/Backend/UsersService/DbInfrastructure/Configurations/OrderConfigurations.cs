using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace UsersService.DbInfrastructure.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(order => order.OrderAddress).HasMaxLength(100);
            builder.Property(order => order.OrderAddress).IsRequired();
            builder.Property(order => order.Note).HasMaxLength(300);

            // brisanjem musterije, brisu se i sve njegove porudzbine
            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.CustomerOrders)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // brisanjem dostavljaca, ne brisu se porudzbine, a dostavljac se postavlja na NULL
            //builder.HasOne(order => order.Deliverer)
            //    .WithMany(deliverer => deliverer.DelivererOrders)
            //    .HasForeignKey(order => order.DelivererId)
            //    .OnDelete(DeleteBehavior.SetNull);

            // brisanjem dostavljaca, ne brisu se porudzbine, ignorise se
            builder.HasOne(order => order.Deliverer)
                .WithMany(deliverer => deliverer.DelivererOrders)
                .HasForeignKey(order => order.DelivererId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
