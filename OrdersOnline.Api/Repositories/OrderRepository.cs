using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersOnline.Api.Data;
using OrdersOnline.Api.Entities;
using OrdersOnline.Api.Repositories.Contracts;
using OrdersOnline.Models.Dto;
using System.Net.Http;
using System.Text.Json;
using System.Text;

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

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _ordersOnlineDbContext.Order
                                                    .Include(o => o.OrderLines)
                                                    .FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }

        public async Task<OrderDTO> UpdateOrderAsync(OrderDTO orderDTO, int id)
        {
            try
            {
                var orderToUpdate = await GetOrderByIdAsync(id);

                if (orderToUpdate == null)
                {
                    throw new ArgumentException($"Order with id {id} does not exist.");
                }

                orderToUpdate.ClientName = orderDTO.ClientName;
                orderToUpdate.OrderPrice = orderDTO.OrderPrice;
                orderToUpdate.Status = (Entities.OrderStatus)orderDTO.Status;
                orderToUpdate.AdditionalInfo = orderDTO.AdditionalInfo;
                orderToUpdate.OrderLines.Clear();

                foreach (var orderLineDTO in orderDTO.OrderLines)
                {
                    var orderLine = new OrderLine
                    {
                        Product = orderLineDTO.Product,
                        Price = orderLineDTO.Price
                    };
                    orderToUpdate.OrderLines.Add(orderLine);
                }

                _ordersOnlineDbContext.Order.Update(orderToUpdate);
                await _ordersOnlineDbContext.SaveChangesAsync();

                return orderDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
    }
}
