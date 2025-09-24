using pos_simple.Model;
using pos_simple.Models;

public class CheckoutItem
{
    public int Id { get; set; }

    // Huruf kecil sesuai database
    public int CheckoutId { get; set; }
    public Checkout Checkout { get; set; } = null!;

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}

public class Checkout
{
    public int Id { get; set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public int StatusOrderId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<CheckoutItem> Items { get; set; } = new();
}
