
using pos_simple.Model;

namespace pos_simple.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Nama produk
        public string Name { get; set; } = string.Empty;

        // Harga modal (capital price)
        public decimal CapitalPrice { get; set; }

        // Harga jual setelah diskon dihitung (akan diisi otomatis di service)
        public decimal Price { get; set; }

        // Relasi ke Category
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Stock
        public int StockActive { get; set; }   // 1 = aktif, 0 = nonaktif
        public int StockQuantity { get; set; } // jumlah stok

        // Diskon
        public int DiscountActive { get; set; }   // 1 = aktif, 0 = nonaktif
        public decimal DiscountValue { get; set; } // bisa persen atau nominal
        public string DiscountType { get; set; } = "none"; // "percent" atau "amount"

        // Status produk
        public int StatusActive { get; set; }  // 1 = aktif, 0 = nonaktif
    }
}
