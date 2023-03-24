namespace OrdersOnline.Api.Entities
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public string? Product { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
