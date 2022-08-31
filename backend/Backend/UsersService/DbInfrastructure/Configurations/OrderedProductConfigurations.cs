using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace UsersService.DbInfrastructure.Configurations
{
    public class OrderedProductConfigurations : IEntityTypeConfiguration<OrderedProduct>
    {
        public void Configure(EntityTypeBuilder<OrderedProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(op => op.Order)
                .WithMany(order => order.OrderedProducts)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
