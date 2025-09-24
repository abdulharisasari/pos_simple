
using pos_simple.Model;

namespace pos_simple.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public decimal CapitalPrice { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; } = null!;

        public int StockActive { get; set; }
        public int StockQuantity { get; set; }

        public int DiscountActive { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; } = "none"; // "percent" | "amount"

        public int StatusActive { get; set; }
    }

}





namespace pos_simple.DTO
{
    public class ProductRequest
    {
        public string Name { get; set; } = string.Empty;

        public decimal CapitalPrice { get; set; }

        public int CategoryId { get; set; }

        public int StockActive { get; set; }
        public int StockQuantity { get; set; }

        public int DiscountActive { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; } = "none";

        public int StatusActive { get; set; }
    }
}
