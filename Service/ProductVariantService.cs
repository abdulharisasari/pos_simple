using Microsoft.EntityFrameworkCore;
using pos_simple.Data;
using pos_simple.DTO;
using pos_simple.Model;

public class ProductVariantService
{
    private readonly AppDbContext _db;

    public ProductVariantService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ProductVariantResponse> CreateAsync(ProductVariantDto request)
    {
        var variant = new ProductVariant
        {
            ProductId = request.ProductId,
            Name = request.Name,
            Price = request.Price
        };

        _db.ProductVariants.Add(variant);
        await _db.SaveChangesAsync();

        return new ProductVariantResponse
        {
            Id = variant.Id,
            ProductId = variant.ProductId,
            Name = variant.Name,
            Price = variant.Price
        };
    }

    public async Task<ProductVariantResponse?> GetByIdAsync(int id)
    {
        var variant = await _db.ProductVariants.FindAsync(id);
        if (variant == null) return null;

        return new ProductVariantResponse
        {
            Id = variant.Id,
            ProductId = variant.ProductId,
            Name = variant.Name,
            Price = variant.Price
        };
    }

    public async Task<List<ProductVariantResponse>> GetAllAsync()
    {
        return await _db.ProductVariants
            .Select(v => new ProductVariantResponse
            {
                Id = v.Id,
                ProductId = v.ProductId,
                Name = v.Name,
                Price = v.Price
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductVariantDto request)
    {
        var variant = await _db.ProductVariants.FindAsync(id);
        if (variant == null) return false;

        variant.ProductId = request.ProductId;
        variant.Name = request.Name;
        variant.Price = request.Price;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var variant = await _db.ProductVariants.FindAsync(id);
        if (variant == null) return false;

        _db.ProductVariants.Remove(variant);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProductVariant>> GetByProductIdAsync(int productId)
    {
        return await _db.ProductVariants
            .Where(v => v.ProductId == productId)
            .Select(v => new ProductVariant
            {
                Id = v.Id,
                ProductId = v.ProductId,
                Name = v.Name,
                Price = v.Price
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateByProductIdAsync(int productId, List<ProductVariantDto> requests)
    {
        var variants = await _db.ProductVariants.Where(v => v.ProductId == productId).ToListAsync();
        if (!variants.Any()) return false;

        for (int i = 0; i < variants.Count && i < requests.Count; i++)
        {
            variants[i].Name = requests[i].Name;
            // update field lain jika ada
        }

        await _db.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteByProductIdAsync(int productId)
    {
        var variants = await _db.ProductVariants.Where(v => v.ProductId == productId).ToListAsync();
        if (!variants.Any()) return false;

        _db.ProductVariants.RemoveRange(variants);
        await _db.SaveChangesAsync();
        return true;
    }
}
