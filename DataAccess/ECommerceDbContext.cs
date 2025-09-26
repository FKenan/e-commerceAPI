using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Farklı sistemlerde (Türkçe, İngilizce vb.) tutarlılık sağlamak için
            var invariantCulture = System.Globalization.CultureInfo.InvariantCulture;

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower(invariantCulture));

                foreach (var property in entity.GetProperties())
                {
                    // Bu satır da "OrderId" gibi sütun adlarını doğru şekilde "orderid" olarak çevirecektir.
                    property.SetColumnName(property.GetColumnName().ToLower(invariantCulture));
                }
            }
        }
    }
}
