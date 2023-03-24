using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersOnline.Models.Dto
{
    public enum OrderStatus
    {
        New,
        Confirm,
        Delivery,
        Cancel
    }

    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }
        public string ClientName { get; set; }
        public decimal OrderPrice { get; set; }
        public string AdditionalInfo { get; set; }
        public List<OrderLineDTO> OrderLines { get; set; }
    }
}
