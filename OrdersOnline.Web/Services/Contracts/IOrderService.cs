﻿using OrdersOnline.Models.Dto;

namespace OrdersOnline.Web.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrders();
        Task<OrderDTO> AddOrder(OrderDTO orderDTO);
    }
}
