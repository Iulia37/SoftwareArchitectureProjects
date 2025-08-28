namespace OrderService.API.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public string MenuItemName { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
