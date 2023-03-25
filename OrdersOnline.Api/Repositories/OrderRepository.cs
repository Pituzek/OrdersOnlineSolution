using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersOnline.Api.Data;
using OrdersOnline.Api.Entities;
using OrdersOnline.Api.Repositories.Contracts;
using OrdersOnline.Models.Dto;

namespace OrdersOnline.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersOnlineDbContext _ordersOnlineDbContext;

        public OrderRepository(OrdersOnlineDbContext ordersOnlineDbContext)
        {
            _ordersOnlineDbContext = ordersOnlineDbContext;
        }

        private async Task<bool> OrderLineExist(int orderId, int orderLineId)
        {
            return await _ordersOnlineDbContext.OrderLines.AnyAsync(c => c.OrderId == orderId && 
                                                                    c.OrderLineId == orderLineId);
        }

        public async Task<Order> AddOrderAsync(OrderDTO orderDTO)
        {
            if (await OrderLineExist(orderDTO.Id, orderDTO.OrderLines.First().Id) == false )
            {
                var newOrder = new Order()
                {
                    OrderPrice = orderDTO.OrderLines.Sum(s => s.Price),
                    AdditionalInfo = orderDTO.AdditionalInfo,
                    ClientName = orderDTO.ClientName,
                    CreateDate = orderDTO.CreateDate,
                    Status = Entities.OrderStatus.New,
                    OrderLines = orderDTO.OrderLines.Select(ol => new OrderLine()
                    {
                        Product = ol.Product,
                        Price = ol.Price,
                        OrderId = orderDTO.Id
                    }).ToList()
                };

                if (newOrder != null)
                {
                    var result = await _ordersOnlineDbContext.Order.AddAsync(newOrder);
                    await _ordersOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var item = await _ordersOnlineDbContext.Order.FindAsync(id);

            if (item != null)
            {
                _ordersOnlineDbContext.Order.Remove(item);
                await _ordersOnlineDbContext.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _ordersOnlineDbContext.Order
                                                     .Include(o => o.OrderLines)
                                                     .ToListAsync();
            return orders;
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
