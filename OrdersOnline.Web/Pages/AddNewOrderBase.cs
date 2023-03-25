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

        protected async Task AddToOrders_Click(OrderDTO orderDTO)
        {
            try
            {
                if (orderDTO.AdditionalInfo == null || orderDTO.AdditionalInfo == "") 
                {
                    OrderDTO.AdditionalInfo = "N/A";
                }

                //NavigationManager.NavigateTo("/"); 
                //OrderDTO = await OrderService.AddOrder(OrderDTO);

                NavigationManager.NavigateTo("/");
                await OrderService.AddOrder(orderDTO);
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

        public async Task HandleValidSubmit()
        {
            //Logger.LogInformation("HandleValidSubmit called");

            // Process the valid form
            await Task.CompletedTask;
        }
    }
}
