using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Data;

public partial class FarmersmarketContext : DbContext
{
    public FarmersmarketContext()
    {
    }

    public FarmersmarketContext(DbContextOptions<FarmersmarketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryStockDetail> CategoryStockDetails { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<StockDetail> StockDetail { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.CategoryName, "UK_lroeo5fvfdeg4hpicn4lw7x9b").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName).HasColumnName("category_name");
        });

        modelBuilder.Entity<CategoryStockDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("category_stock_details");

            entity.HasIndex(e => e.CategoryCategoryId, "FKkxgka352hmpoloeoep7cm9b7c");

            entity.HasIndex(e => e.StockDetailsProductId, "UK_oreb29n1k4brb9cn4fy50l7f7").IsUnique();

            entity.Property(e => e.CategoryCategoryId).HasColumnName("category_category_id");
            entity.Property(e => e.StockDetailsProductId).HasColumnName("stock_details_product_id");

            entity.HasOne(d => d.CategoryCategory).WithMany()
                .HasForeignKey(d => d.CategoryCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKkxgka352hmpoloeoep7cm9b7c");

            entity.HasOne(d => d.StockDetailsProduct).WithOne()
                .HasForeignKey<CategoryStockDetail>(d => d.StockDetailsProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKw6smw42ssadb9q6kxc291mf8");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PRIMARY");

            entity.ToTable("farmer");

            entity.HasIndex(e => e.PhoneNo, "UK_o75u7w6s9t2ao4doco3vf238e").IsUnique();

            entity.Property(e => e.FarmerId).HasColumnName("farmer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(20)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(20)
                .HasColumnName("lastname");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .HasColumnName("phone_no");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.UserId, "FKel9kyl84ego2otj2accfd8mr7");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.DeliveryStatus)
                .HasColumnType("bit(1)")
                .HasColumnName("delivery_status");
            entity.Property(e => e.PaymentStatus)
                .HasColumnType("bit(1)")
                .HasColumnName("payment_status");
            entity.Property(e => e.PlaceOrderDate).HasColumnName("place_order_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKel9kyl84ego2otj2accfd8mr7");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_details");

            entity.HasIndex(e => e.FarmerId, "FKe42r1gblmc7u54u3geikk7ig4");

            entity.HasIndex(e => e.OrderId, "FKjyu2qbqt8gnvno9oe9j2s2ldk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.FarmerId).HasColumnName("farmer_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderItem)
                .HasMaxLength(20)
                .HasColumnName("order_item");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Farmer).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FKe42r1gblmc7u54u3geikk7ig4");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKjyu2qbqt8gnvno9oe9j2s2ldk");
        });

        modelBuilder.Entity<StockDetail>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("stock_details");

            entity.HasIndex(e => e.CategoryId, "FKikv0jeil5f6snxpkqkk6itk56");

            entity.HasIndex(e => e.FarmerId, "FKti5jc581t2ne5x55q9nq3fqpk");

            entity.HasIndex(e => e.StockItem, "UK_19kumlh2b3wbeboabeekd6wf9").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.FarmerId).HasColumnName("farmer_id");
            entity.Property(e => e.PricePerUnit).HasColumnName("price_per_unit");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(400)
                .HasColumnName("product_image");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.StockItem)
                .HasMaxLength(50)
                .HasColumnName("stock_item");

            entity.HasOne(d => d.Category).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FKikv0jeil5f6snxpkqkk6itk56");

            entity.HasOne(d => d.Farmer).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.FarmerId)
                .HasConstraintName("FKti5jc581t2ne5x55q9nq3fqpk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UK_ob8kqyqqgmefl0aco34akdtpe").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(20)
                .HasColumnName("firstname");
            entity.Property(e => e.IsAdmin)
                .HasColumnType("bit(1)")
                .HasColumnName("is_admin");
            entity.Property(e => e.Lastname)
                .HasMaxLength(20)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(15)
                .HasColumnName("phone_no");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
