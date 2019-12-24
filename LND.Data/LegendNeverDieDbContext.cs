using LND.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LND.Data
{
    public class LegendNeverDieDbContext : IdentityDbContext<ApplicationUser>
    {
        public LegendNeverDieDbContext() : base("connect")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<OrderAdmin> OrderAdmins { get; set; }

        public static LegendNeverDieDbContext Create()
        {
            return new LegendNeverDieDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId);
        }
    }
}