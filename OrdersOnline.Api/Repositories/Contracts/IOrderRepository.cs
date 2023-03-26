using OrdersOnline.Api.Entities;
using OrdersOnline.Models.Dto;

namespace OrdersOnline.Api.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> AddOrderAsync(OrderDTO order);
        Task<OrderDTO> UpdateOrderAsync(OrderDTO order, int id);
        Task DeleteOrderAsync(int id);
    }
}
