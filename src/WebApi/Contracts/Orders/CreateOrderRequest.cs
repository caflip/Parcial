namespace WebApi.Contracts.Orders
{
    public class CreateOrderRequest
    {
        public required string Customer { get; set; }
        public required string Producto { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
