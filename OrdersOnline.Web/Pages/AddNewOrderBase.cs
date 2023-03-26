using Microsoft.AspNetCore.Components;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;

namespace OrdersOnline.Web.Pages
{
    public class AddNewOrderBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public OrderDTO OrderDTO { get; set; } 

        protected override async Task OnInitializedAsync()
        {
            OrderDTO = new OrderDTO();
            OrderDTO.OrderLines = new List<OrderLineDTO>();
        }

        public async Task AddToOrders_Click(OrderDTO orderDTO)
        {
            try
            {

                if (orderDTO.AdditionalInfo == null || orderDTO.AdditionalInfo == "")
                {
                    OrderDTO.AdditionalInfo = "N/A";
                }

                await OrderService.AddOrder(orderDTO);

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
