using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace OrdersOnline.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<OrderDTO>> GetItems()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Order");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<OrderDTO>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<OrderDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }
    }
}
