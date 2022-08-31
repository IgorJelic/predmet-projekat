using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;

namespace UsersService.DbInfrastructure.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(user => user.FirstName).HasMaxLength(30);
            builder.Property(user => user.FirstName).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(60);
            builder.Property(user => user.LastName).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(320);
            builder.Property(user => user.Email).IsRequired();
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.Password).IsRequired();
            builder.Property(user => user.DateOfBirth).IsRequired();
        }
    }
}
