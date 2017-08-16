using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using YSBoxing;

namespace YSBoxing.Migrations
{
    [DbContext(typeof(YSDbContext))]
    [Migration("20170808013706_updateMaxQty")]
    partial class updateMaxQty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YSBoxing.Core.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("CustomerName");

                    b.Property<bool>("HasMixStyle");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("YSBoxing.Core.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("CustomerId");

                    b.Property<string>("JcNo")
                        .HasMaxLength(20);

                    b.Property<int>("OrderGroupId");

                    b.Property<string>("Style")
                        .HasMaxLength(20);

                    b.Property<string>("StyleDescription")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderGroupId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("YSBoxing.Core.OrderGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("GroupDescription");

                    b.Property<string>("GroupName");

                    b.HasKey("Id");

                    b.ToTable("OrderGroup");
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Box");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("HasMixStyle");

                    b.Property<int>("OrderId");

                    b.Property<string>("OrderInfo1")
                        .HasMaxLength(20);

                    b.Property<string>("OrderInfo2")
                        .HasMaxLength(20);

                    b.Property<string>("OrderInfo3")
                        .HasMaxLength(20);

                    b.Property<string>("OrderItemName")
                        .HasMaxLength(20);

                    b.Property<string>("OrderOtherInfo")
                        .HasMaxLength(255);

                    b.Property<int>("Qty");

                    b.Property<int>("ShipBox");

                    b.Property<int>("ShipQty");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemBoxing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoxCode");

                    b.Property<int>("BoxNumber");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsShip");

                    b.Property<int>("OrderItemId");

                    b.Property<int>("Qty");

                    b.Property<DateTime>("ShipDate");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItemBoxings");
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemBoxingQty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("OrderItemBoxingId");

                    b.Property<int>("Qty");

                    b.Property<string>("Size")
                        .HasMaxLength(20);

                    b.Property<string>("Style")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("OrderItemBoxingId");

                    b.ToTable("OrderItemBoxingQtys");
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemQty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoxingQty");

                    b.Property<string>("Color")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("OrderItemId");

                    b.Property<string>("OtherInfo")
                        .HasMaxLength(255);

                    b.Property<int>("Qty");

                    b.Property<int>("ShipQty");

                    b.Property<string>("Size")
                        .HasMaxLength(20);

                    b.Property<string>("Style")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId");

                    b.ToTable("OrderItemQtys");
                });

            modelBuilder.Entity("YSBoxing.Core.PackingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("MaxQty");

                    b.Property<int>("MaxQtyBig");

                    b.Property<string>("Size")
                        .HasMaxLength(20);

                    b.Property<string>("Style")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("PackingItems");
                });

            modelBuilder.Entity("YSBoxing.Core.YS.InputYsOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressCode");

                    b.Property<string>("AddressName");

                    b.Property<string>("Color");

                    b.Property<int>("CustomerId");

                    b.Property<bool>("IsGenerated");

                    b.Property<int>("OrderGroupId");

                    b.Property<int>("Qty1");

                    b.Property<int>("Qty2");

                    b.Property<int>("Qty3");

                    b.Property<int>("Qty4");

                    b.Property<string>("Style");

                    b.Property<string>("StyleDescription");

                    b.Property<string>("YsGoodNo");

                    b.Property<string>("YsOrderNo");

                    b.Property<int>("qty10");

                    b.Property<int>("qty5");

                    b.Property<int>("qty6");

                    b.Property<int>("qty7");

                    b.Property<int>("qty8");

                    b.Property<int>("qty9");

                    b.HasKey("Id");

                    b.ToTable("InputYsOrders");
                });

            modelBuilder.Entity("YSBoxing.Core.Order", b =>
                {
                    b.HasOne("YSBoxing.Core.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("YSBoxing.Core.OrderGroup", "OrderGroup")
                        .WithMany("Orders")
                        .HasForeignKey("OrderGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItem", b =>
                {
                    b.HasOne("YSBoxing.Core.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemBoxing", b =>
                {
                    b.HasOne("YSBoxing.Core.OrderItem", "OrderItem")
                        .WithMany("OrderItemBoxings")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemBoxingQty", b =>
                {
                    b.HasOne("YSBoxing.Core.OrderItemBoxing", "OrderItemBoxing")
                        .WithMany("OrderItemBoxingQtys")
                        .HasForeignKey("OrderItemBoxingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("YSBoxing.Core.OrderItemQty", b =>
                {
                    b.HasOne("YSBoxing.Core.OrderItem", "OrderItem")
                        .WithMany("OrderItemQtys")
                        .HasForeignKey("OrderItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
