using Microsoft.EntityFrameworkCore;
using WebApi_Examinationen.Models.Order;
using WebApi_Examinationen.Models.Product;
using WebApi_Examinationen.Models.User;

namespace WebApi_Examinationen.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;
        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<OrderEntity> Orders { get; set; } = null!;
        public virtual DbSet<OrderRowEntity> OrderRows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderRowEntity>()
                .HasKey(c => new { c.OrderId, c.ArticleNumber });
        }
    }
}
