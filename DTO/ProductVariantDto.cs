namespace pos_simple.DTO
{
    public class ProductVariantDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }
    }

    public class ProductVariantResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }
    }
}
