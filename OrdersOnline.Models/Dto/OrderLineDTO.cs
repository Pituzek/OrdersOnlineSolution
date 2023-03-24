using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersOnline.Models.Dto
{
    public class OrderLineDTO
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
    }
}
