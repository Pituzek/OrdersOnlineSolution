using Microsoft.EntityFrameworkCore;
using OrdersOnline.Api.Data;
using OrdersOnline.Api.Entities;
using OrdersOnline.Api.Repositories.Contracts;

namespace OrdersOnline.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersOnlineDbContext _ordersOnlineDbContext;

        public OrderRepository(OrdersOnlineDbContext ordersOnlineDbContext)
        {
            _ordersOnlineDbContext = ordersOnlineDbContext;
        }

        public Task AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            //throw new NotImplementedException();
            var orders = await _ordersOnlineDbContext.Order
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
