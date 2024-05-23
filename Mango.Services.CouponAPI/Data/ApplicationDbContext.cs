using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "100FF",
            DiscountAmount = 10,
            MinAmount = 20
        });

        builder.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 2,
            CouponCode = "200FF",
            DiscountAmount = 20,
            MinAmount = 40
        });
    }
}