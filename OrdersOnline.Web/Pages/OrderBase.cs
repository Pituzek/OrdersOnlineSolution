using Microsoft.AspNetCore.Components;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;

namespace OrdersOnline.Web.Pages
{
    public class OrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        public IEnumerable<OrderDTO> Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Orders = await OrderService.GetOrders();
        }

        public void DeleteOrder(OrderDTO order)
        {
            OrderService.DeleteOrder(order);
        }
    }
}
