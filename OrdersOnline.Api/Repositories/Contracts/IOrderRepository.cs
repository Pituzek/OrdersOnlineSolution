using OrdersOnline.Api.Entities;
using OrdersOnline.Models.Dto;

namespace OrdersOnline.Api.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> AddOrderAsync(OrderDTO order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
