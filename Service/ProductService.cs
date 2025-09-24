

using Microsoft.EntityFrameworkCore;
using pos_simple.Data;
using pos_simple.DTO;
using pos_simple.Model;
using pos_simple.Models;

namespace pos_simple.Service
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        private decimal CalculateFinalPrice(Product product)
        {
            decimal finalPrice = product.CapitalPrice;

            if (product.DiscountActive == 1)
            {
                if (product.DiscountType == "percent")
                {
                    finalPrice = product.CapitalPrice - (product.CapitalPrice * (product.DiscountValue / 100));
                }
                else if (product.DiscountType == "amount")
                {
                    finalPrice = product.CapitalPrice - product.DiscountValue;
                }
            }

            return finalPrice < 0 ? 0 : finalPrice;
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                CapitalPrice = product.CapitalPrice,
                Price = CalculateFinalPrice(product),
                Category = product.Category,
                StockActive = product.StockActive,
                StockQuantity = product.StockQuantity,
                DiscountActive = product.DiscountActive,
                DiscountValue = product.DiscountValue,
                DiscountType = product.DiscountType,
                StatusActive = product.StatusActive
            };
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _db.Products.Include(p => p.Category).ToListAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                CapitalPrice = request.CapitalPrice,
                CategoryId = request.CategoryId,
                StockActive = request.StockActive,
                StockQuantity = request.StockQuantity,
                DiscountActive = request.DiscountActive,
                DiscountValue = request.DiscountValue,
                DiscountType = request.DiscountType,
                StatusActive = request.StatusActive
            };

            product.Price = CalculateFinalPrice(product);

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            await _db.Entry(product).Reference(p => p.Category).LoadAsync();
            return MapToDto(product);
        }

        public async Task<ProductDto?> UpdateAsync(int id, ProductRequest request)
        {
            var existing = await _db.Products.FindAsync(id);
            if (existing == null) return null;

            existing.Name = request.Name;
            existing.CapitalPrice = request.CapitalPrice;
            existing.CategoryId = request.CategoryId;
            existing.StockActive = request.StockActive;
            existing.StockQuantity = request.StockQuantity;
            existing.DiscountActive = request.DiscountActive;
            existing.DiscountValue = request.DiscountValue;
            existing.DiscountType = request.DiscountType;
            existing.StatusActive = request.StatusActive;

            existing.Price = CalculateFinalPrice(existing);

            await _db.SaveChangesAsync();
            await _db.Entry(existing).Reference(p => p.Category).LoadAsync();

            return MapToDto(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _db.Products.FindAsync(id);
            if (existing == null) return false;

            _db.Products.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
