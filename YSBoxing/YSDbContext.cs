using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSBoxing.Core;
using YSBoxing.Core.YS;

namespace YSBoxing
{
    public class YSDbContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderGroup> OrderGroup { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemBoxing> OrderItemBoxings { get; set; }
        public DbSet<OrderItemBoxingQty> OrderItemBoxingQtys { get; set; }
        public DbSet<OrderItemQty> OrderItemQtys { get; set; }
        public DbSet<PackingItem> PackingItems { get; set; }


        public DbSet<InputYsOrder> InputYsOrders { get; set; }


        public YSDbContext(DbContextOptions<YSDbContext> options) : base(options)
        {
            

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
