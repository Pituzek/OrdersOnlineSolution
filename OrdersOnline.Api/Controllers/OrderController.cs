using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrdersOnline.Api.Entities;
using OrdersOnline.Api.Extensions;
using OrdersOnline.Api.Repositories.Contracts;
using OrdersOnline.Models.Dto;
using System.Linq;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            try
            {
                var orderToUpdate = await _orderRepository.GetOrderByIdAsync(id);

                if (orderToUpdate != null)
                {
                    var order = orderToUpdate.ConvertToDto(orderToUpdate);
                    return Ok(order);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                var orderLines = orders.SelectMany(o => o.OrderLines);

                if (orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var ordersDtos = orders.ConvertToDto(orderLines.ToList());

                    return Ok(ordersDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");

            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var newOrder = await _orderRepository.AddOrderAsync(orderDTO);

                if (newOrder == null)
                {
                    return NoContent();
                }

                var newOrderLines = orderDTO.OrderLines.Select(ol => new OrderLine()
                {
                    Product = ol.Product,
                    Price = ol.Price
                }).ToList();

                foreach (var orderLine in newOrderLines)
                {
                    newOrder.OrderLines.Add(orderLine);
                }

                var newOrderDTO = newOrder.ConvertToDto(newOrder);

                return Ok(newOrderDTO);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {

                await _orderRepository.DeleteOrderAsync(id);

                return Ok();

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder( int id, [FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = await _orderRepository.UpdateOrderAsync(orderDTO, id);

                return Ok(order);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
