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
            try
            {
                int orderId = Convert.ToInt32(id);
                var order = await OrderService.GetOrder(orderId);

                OrderDTO = new OrderDTO
                {
                    Id = order.Id,
                    CreateDate = order.CreateDate,
                    Status = order.Status,
                    ClientName = order.ClientName,
                    OrderPrice = order.OrderPrice,
                    AdditionalInfo = order.AdditionalInfo,
                    OrderLines = order.OrderLines.Select(ol => new OrderLineDTO
                    {
                        Id = ol.Id,
                        Product = ol.Product,
                        Price = ol.Price
                    }).ToList()
                };

                if (OrderDTO == null)
                {
                    throw new Exception($"Order with id {orderId} not found.");
                }
            }
            catch (Exception)
            {

                throw;
            }
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
