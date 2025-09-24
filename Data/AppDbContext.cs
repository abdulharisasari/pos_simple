using Microsoft.EntityFrameworkCore;
using pos_simple.Model;
using pos_simple.Models;

namespace pos_simple.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutItem> CheckoutItems { get; set; } = null!;
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Username).HasColumnName("username");
                // tambahkan kolom lain sesuai tabel
            });

            modelBuilder.Entity<ProductVariant>(entity =>
            {
                entity.ToTable("product_variants");
                entity.Property(pv => pv.Id).HasColumnName("id");
                entity.Property(pv => pv.ProductId).HasColumnName("productid");
                entity.Property(pv => pv.Name).HasColumnName("name");
                // tambahkan kolom lain sesuai tabel
            });
            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.CapitalPrice).HasColumnName("capital_price");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                // Stock
                entity.Property(e => e.StockActive).HasColumnName("stock_active");
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");

                // Discount
                entity.Property(e => e.DiscountActive).HasColumnName("discount_active");
                entity.Property(e => e.DiscountValue).HasColumnName("discount_value");
                entity.Property(e => e.DiscountType).HasColumnName("discount_type");

                // Status
                entity.Property(e => e.StatusActive).HasColumnName("status_active");
            });

            // PaymentMethod
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_methods");
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Name).HasColumnName("name");
            });

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Cash" },
                new PaymentMethod { Id = 2, Name = "Debit" }
            );

            // Checkout
            modelBuilder.Entity<Checkout>(entity =>
            {
                entity.ToTable("checkouts");

                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.PaymentMethodId).HasColumnName("payment_method_id");
                entity.Property(c => c.StatusOrderId).HasColumnName("status_order_id");
                entity.Property(c => c.TotalPrice).HasColumnName("totalprice");
                entity.Property(c => c.CreatedAt)
                      .HasColumnName("createdat")
                      .HasColumnType("timestamp with time zone"); // ubah ke 'with time zone'
            });


            // CheckoutItem
            modelBuilder.Entity<CheckoutItem>(entity =>
            {
                entity.ToTable("checkout_items");

                entity.Property(ci => ci.Id).HasColumnName("id");
                entity.Property(ci => ci.CheckoutId).HasColumnName("checkoutid");
                entity.Property(ci => ci.ProductId).HasColumnName("productid");
                entity.Property(ci => ci.Quantity).HasColumnName("quantity");
                entity.Property(ci => ci.SubTotal).HasColumnName("subtotal");

                entity.HasOne(ci => ci.Checkout)
                      .WithMany(c => c.Items)
                      .HasForeignKey(ci => ci.CheckoutId);

                entity.HasOne(ci => ci.Product)
                      .WithMany()
                      .HasForeignKey(ci => ci.ProductId);
            });


            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.Name).HasColumnName("name");
            });
        }
    }
}
