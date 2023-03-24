using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersOnline.Api.Entities;
using OrdersOnline.Models.Dto;

namespace OrdersOnline.Api.Extensions
{
    [Route("api/[controller]")]
    [ApiController]
    public static class DtoConversions
    {
        public static IEnumerable<OrderDTO> ConvertToDto(this IEnumerable<Order> orders,
                                                              IEnumerable<OrderLine> orderLines)
        {
            var orderLinesByOrderId = orderLines.GroupBy(ol => ol.OrderId);

            return (from order in orders
                    join orderLinesGroup in orderLinesByOrderId
                    on order.OrderId equals orderLinesGroup.Key
                    select new OrderDTO
                    {
                        Id = order.OrderId,
                        CreateDate = order.CreateDate,
                        ClientName = order.ClientName,
                        OrderPrice = order.OrderPrice,
                        AdditionalInfo = order.AdditionalInfo,
                        OrderLines = orderLinesGroup.Select(ol => new OrderLineDTO
                        {
                            Id = ol.OrderLineId,
                            Product = ol.Product,
                            Price = ol.Price
                        }).ToList()
                    });
        }
    }
}
