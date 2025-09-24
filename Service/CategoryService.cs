using Microsoft.EntityFrameworkCore;
using pos_simple.Data;
using pos_simple.Model;
using pos_simple.Models;   // <- harus pakai Models, bukan Model

namespace pos_simple.Service
{
    public class CategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        // Get all categories
        public async Task<List<Category>> GetAllAsync()
            => await _db.Categories.ToListAsync();

        // ✅ Create category (bukan checkout)
        public async Task<Category> CreateAsync(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category;
        }

        // Update category
        public async Task<bool> UpdateAsync(int id, Category category)
        {
            var existing = await _db.Categories.FindAsync(id);
            if (existing == null) return false;

            existing.Name = category.Name;
            await _db.SaveChangesAsync();
            return true;
        }

        // Delete category
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
