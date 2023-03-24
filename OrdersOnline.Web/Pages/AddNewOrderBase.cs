using Microsoft.AspNetCore.Components;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;
using OrdersOnline.Web.Services;

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

        protected async Task AddToOrders_Click(OrderDTO orderDTO)
        {
            try
            {
                if (OrderDTO.AdditionalInfo == null || OrderDTO.AdditionalInfo == "") 
                {
                    OrderDTO.AdditionalInfo = "N/A";
                }

                NavigationManager.NavigateTo("/");

                OrderDTO = await OrderService.AddOrder(OrderDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OrderLineDTO> orderLines = new List<OrderLineDTO>();

        public void AddOrderLine()
        {
            OrderDTO.OrderLines.Add(new OrderLineDTO());
            StateHasChanged();
        }
        public void RemoveOrderLine(int index)
        {
            OrderDTO.OrderLines.RemoveAt(index);
        }
    }
}
