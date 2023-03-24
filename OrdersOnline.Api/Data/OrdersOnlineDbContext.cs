using Microsoft.EntityFrameworkCore;
using OrdersOnline.Api.Entities;

namespace OrdersOnline.Api.Data
{
    public class OrdersOnlineDbContext : DbContext
    {
        public OrdersOnlineDbContext(DbContextOptions<OrdersOnlineDbContext> options)
            : base(options)
        { 
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Initial sample orders
            var orders = new[]
            {
                    new Order
                    {
                        OrderId = 1,
                        CreateDate = DateTime.Now.AddDays(-1),
                        Status = OrderStatus.New,
                        ClientName = "John Smith",
                        OrderPrice = 50.00m,
                        AdditionalInfo = "N/A",
                    },
                    new Order
                    {
                        OrderId = 2,
                        CreateDate = DateTime.Now.AddDays(-2),
                        Status = OrderStatus.New,
                        ClientName = "Jane Doe",
                        OrderPrice = 75.00m,
                        AdditionalInfo = "Will be cancelled due to product unavailability",
                    },
                    new Order
                    {
                        OrderId = 3,
                        CreateDate = DateTime.Now.AddDays(-3),
                        Status = OrderStatus.New,
                        ClientName = "Bob Johnson",
                        OrderPrice = 100.00m,
                        AdditionalInfo = "Will be delivered to front desk",
                    }
            };

            var orderLines = new[] {
                    new OrderLine 
                    { 
                        OrderId = 1, 
                        OrderLineId = 1, 
                        Product = "Product A", 
                        Price = 25.00m 
                    },
                    new OrderLine 
                    { 
                        OrderId = 1, 
                        OrderLineId = 2, 
                        Product = "Product B", 
                        Price = 25.00m 
                    },
                    new OrderLine 
                    { 
                        OrderId = 2, 
                        OrderLineId = 3, 
                        Product = "Product C", 
                        Price = 50.00m 
                    },
                    new OrderLine 
                    { 
                        OrderId = 2, 
                        OrderLineId = 4, 
                        Product = "Product D", 
                        Price = 25.00m 
                    },
                    new OrderLine 
                    { 
                        OrderId = 3, 
                        OrderLineId = 5, 
                        Product = "Product E", 
                        Price = 50.00m 
                    },
                    new OrderLine
                    {
                        OrderId = 3, 
                        OrderLineId = 6, 
                        Product = "Product F", 
                        Price = 50.00m 
                    }
            };

            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<OrderLine>().HasData(orderLines);
        }
    }
}
