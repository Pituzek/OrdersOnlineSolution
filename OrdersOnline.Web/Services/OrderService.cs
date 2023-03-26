using Newtonsoft.Json;
using OrdersOnline.Models.Dto;
using OrdersOnline.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrdersOnline.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDTO> GetOrder(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Order/{id}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new OrderDTO();
                    }

                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    };

                    return await response.Content.ReadFromJsonAsync<OrderDTO>(options);
                    //var json = await response.Content.ReadAsStringAsync();
                    //return JsonConvert.DeserializeObject<OrderDTO>(json);
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
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDTO> DeleteOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Order/{orderDTO.Id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<OrderDTO>();
                }

                return default(OrderDTO);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OrderDTO> UpdateOrder(OrderDTO orderDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/Orders/" + orderDTO.Id, orderDTO);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to update order. Status code: {response.StatusCode}");
                }

                return await response.Content.ReadFromJsonAsync<OrderDTO>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
