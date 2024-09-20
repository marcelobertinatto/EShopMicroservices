using Discount.gRPC._2___Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC._3___Infra
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "iPhone X", Description = "iPhone Discount", Amount = 150},
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
                );
        }
    }
}
