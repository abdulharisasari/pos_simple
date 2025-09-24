
namespace pos_simple.DTO
{
    public class CheckoutDto
    {
        public int PaymentMethodId { get; set; }
        public int StatusOrderId { get; set; }
        public List<CheckoutItemDto> Items { get; set; } = new();
    }


}


public class CheckoutResponse
{
    public int Id { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int StatusOrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CheckoutItemResponse> Items { get; set; } = new();
}
