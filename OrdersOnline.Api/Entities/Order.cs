namespace OrdersOnline.Api.Entities
{
    public enum OrderStatus
    {
        New,
        Confirm,
        Delivery,
        Cancel
    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderStatus Status { get; set; }
        public string? ClientName { get; set; }
        public decimal OrderPrice { get; set; }
        public string? AdditionalInfo { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
