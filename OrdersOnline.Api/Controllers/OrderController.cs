using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersOnline.Api.Extensions;
using OrdersOnline.Api.Repositories.Contracts;
using OrdersOnline.Models.Dto;
using System.Reflection.Metadata.Ecma335;

namespace OrdersOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders() 
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                var orderLines = orders.Select(o => o.OrderLines);

                if (orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var ordersDtos = orders.ConvertToDto((IEnumerable<Entities.OrderLine>)orderLines);

                    return Ok(ordersDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
                
            }
        }
    }
}
