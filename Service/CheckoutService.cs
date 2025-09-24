using Microsoft.EntityFrameworkCore;
using pos_simple.Data;
using pos_simple.DTO;
using pos_simple.Models;

namespace pos_simple.Service
{
    public class CheckoutService
    {
        private readonly AppDbContext _db;

        public CheckoutService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CheckoutResponse> CreateAsync(CheckoutDto request)
        {
            var checkout = new Checkout
            {
                PaymentMethodId = request.PaymentMethodId,
                StatusOrderId = request.StatusOrderId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = 0
            };

            _db.Checkouts.Add(checkout);
            await _db.SaveChangesAsync(); // dapat Checkout.Id

            decimal totalPrice = 0;

            foreach (var item in request.Items)
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product == null)
                    throw new Exception($"Product with id {item.ProductId} not found");

                var subTotal = product.Price * item.Quantity;
                totalPrice += subTotal;

                _db.CheckoutItems.Add(new CheckoutItem
                {
                    CheckoutId = checkout.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    SubTotal = subTotal
                });

                if (product.StockActive == 1)
                {
                    if (product.StockQuantity < item.Quantity)
                        throw new Exception($"Stock not enough for product {product.Name}");

                    product.StockQuantity -= item.Quantity;
                }
            }

            // Update total price
            checkout.TotalPrice = totalPrice;
            await _db.SaveChangesAsync();

            // 4. Load relasi untuk response
            await _db.Entry(checkout).Reference(c => c.PaymentMethod).LoadAsync();
            await _db.Entry(checkout).Collection(c => c.Items).Query().Include(i => i.Product).LoadAsync();

            return MapToResponse(checkout);
        }

        public async Task<CheckoutResponse?> GetByIdAsync(int id)
        {
            var checkout = await _db.Checkouts
                .Include(c => c.PaymentMethod)
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            return checkout == null ? null : MapToResponse(checkout);
        }

        public async Task<List<CheckoutResponse>> GetAllAsync()
        {
            var checkouts = await _db.Checkouts
                .Include(c => c.PaymentMethod)
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            return checkouts.Select(MapToResponse).ToList();
        }

        public async Task<CheckoutResponse?> UpdateAsync(int id, CheckoutDto request)
        {
            var checkout = await _db.Checkouts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checkout == null) return null;

            // Update status order selalu
            checkout.StatusOrderId = request.StatusOrderId;

            if (request.Items != null && request.Items.Any())
            {
                // Rollback stok lama
                foreach (var oldItem in checkout.Items)
                {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == oldItem.ProductId);
                    if (product != null && product.StockActive == 1)
                        product.StockQuantity += oldItem.Quantity;
                }

                // Hapus item lama
                _db.CheckoutItems.RemoveRange(checkout.Items);

                // Tambahkan item baru
                decimal totalPrice = 0;
                foreach (var item in request.Items)
                {
                    var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                    if (product == null)
                        throw new Exception($"Product with id {item.ProductId} not found");

                    if (product.StockActive == 1 && product.StockQuantity < item.Quantity)
                        throw new Exception($"Stock not enough for product {product.Name}");

                    if (product.StockActive == 1)
                        product.StockQuantity -= item.Quantity;

                    var subTotal = product.Price * item.Quantity;
                    totalPrice += subTotal;

                    _db.CheckoutItems.Add(new CheckoutItem
                    {
                        CheckoutId = checkout.Id,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        SubTotal = subTotal
                    });
                }

                checkout.TotalPrice = totalPrice;
            }

            await _db.SaveChangesAsync();

            await _db.Entry(checkout).Reference(c => c.PaymentMethod).LoadAsync();
            await _db.Entry(checkout).Collection(c => c.Items).Query().Include(i => i.Product).LoadAsync();

            return MapToResponse(checkout);
        }


        private CheckoutResponse MapToResponse(Checkout checkout)
        {
            return new CheckoutResponse
            {
                Id = checkout.Id,
                PaymentMethod = checkout.PaymentMethod?.Name ?? "Unknown",
                StatusOrderId = checkout.StatusOrderId,
                TotalPrice = checkout.TotalPrice,
                CreatedAt = checkout.CreatedAt,
                Items = checkout.Items.Select(i => new CheckoutItemResponse
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? string.Empty,
                    Quantity = i.Quantity,
                    SubTotal = i.SubTotal
                }).ToList()
            };
        }
    }
}
