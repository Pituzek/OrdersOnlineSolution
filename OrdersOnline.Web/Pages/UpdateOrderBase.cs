using Microsoft.AspNetCore.Components;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services;
using OrdersOnline.Web.Services.Contracts;

namespace OrdersOnline.Web.Pages
{
    public class UpdateOrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string id { get; set; }

        public OrderDTO OrderDTO { get; set; }
        public object UriHelper { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            OrderDTO = new OrderDTO();
            OrderDTO = await OrderService.GetOrder(Convert.ToInt32(id));
            //OrderDTO.OrderLines = new List<OrderLineDTO>();
        }

        public async Task UpdateOrders(OrderDTO orderDTO)
        {
            try
            {
                await OrderService.UpdateOrder(orderDTO);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<OrderLineDTO> orderLines = new List<OrderLineDTO>();

        public void AddOrderLine()
        {
            OrderDTO.OrderLines.Add(new OrderLineDTO());
            StateHasChanged();
        }

        public async Task RemoveOrderLine(int index)
        {
            OrderDTO.OrderLines.RemoveAt(index);
        }
    }
}
