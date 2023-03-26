using OrdersOnline.Models.Dto;

namespace OrdersOnline.Web.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> AddOrder(OrderDTO orderDTO);
        Task<OrderDTO> DeleteOrder(OrderDTO orderDTO);
        Task<OrderDTO> UpdateOrder(OrderDTO orderDTO);
        Task<OrderDTO> GetOrder(int id);
    }
}
