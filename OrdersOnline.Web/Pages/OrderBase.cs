﻿using Microsoft.AspNetCore.Components;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;

namespace OrdersOnline.Web.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        public List<OrderDTO>? Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Orders = (List<OrderDTO>)await OrderService.GetOrders();
        }

        public async Task DeleteOrder(OrderDTO order)
        {
            Orders.RemoveAll(x => x.Id == order.Id);

            await InvokeAsync(() =>
            {
                OrderService.DeleteOrder(order);
                this.StateHasChanged();
            });
        }

        public async Task<List<OrderDTO>> GetOrderList()
        {
            return Orders;
        }

        public async Task UpdateOrderList(OrderDTO order)
        {
            await InvokeAsync(() => {

                if (order != null) { 

                Orders.Add(order);
                this.StateHasChanged();

                }

            });
        }

        public OrderBase Get()
        {
            return this;
        }
    }
}
