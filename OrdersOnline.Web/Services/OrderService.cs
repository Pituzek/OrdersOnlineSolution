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

        public async Task<IEnumerable<OrderDTO>> GetOrders()
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

        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<OrderDTO>("api/Order", orderDTO);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(OrderDTO);
                    }

                    return await response.Content.ReadFromJsonAsync<OrderDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status: {response.StatusCode} Message-{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
