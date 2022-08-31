﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UsersService.DbInfrastructure;

namespace UsersService.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20220831120600_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UsersService.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Confirmed")
                        .HasColumnType("bit");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<long?>("DelivererId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("OrderAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DelivererId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("UsersService.Models.OrderedProduct", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderedProducts");
                });

            modelBuilder.Entity("UsersService.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfileStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UsersService.Models.Order", b =>
                {
                    b.HasOne("UsersService.Models.User", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UsersService.Models.User", "Deliverer")
                        .WithMany("DelivererOrders")
                        .HasForeignKey("DelivererId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Customer");

                    b.Navigation("Deliverer");
                });

            modelBuilder.Entity("UsersService.Models.OrderedProduct", b =>
                {
                    b.HasOne("UsersService.Models.Order", "Order")
                        .WithMany("OrderedProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("UsersService.Models.Order", b =>
                {
                    b.Navigation("OrderedProducts");
                });

            modelBuilder.Entity("UsersService.Models.User", b =>
                {
                    b.Navigation("CustomerOrders");

                    b.Navigation("DelivererOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
