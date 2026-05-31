using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoC.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PickupPoint> PickupPoints { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=111");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("accounts_pkey");

            entity.ToTable("accounts", "demo");

            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories", "demo");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Manufacturerid).HasName("manufacturers_pkey");

            entity.ToTable("manufacturers", "demo");

            entity.Property(e => e.Manufacturerid).HasColumnName("manufacturerid");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("orders_pkey");

            entity.ToTable("orders", "demo");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CodeTake).HasColumnName("code_take");
            entity.Property(e => e.DateDelivery).HasColumnName("date_delivery");
            entity.Property(e => e.DateOrder).HasColumnName("date_order");
            entity.Property(e => e.PickupPointId).HasColumnName("pickup_point_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_account_id_fkey");

            entity.HasOne(d => d.PickupPoint).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PickupPointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_pickup_point_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_status_id_fkey");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("order_detail_pkey");

            entity.ToTable("order_detail", "demo");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.CountProduct).HasColumnName("count_product");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_product_id_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("order_status_pkey");

            entity.ToTable("order_status", "demo");

            entity.Property(e => e.OrderStatusId).HasColumnName("order_status_id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<PickupPoint>(entity =>
        {
            entity.HasKey(e => e.PickupPointid).HasName("pickup_points_pkey");

            entity.ToTable("pickup_points", "demo");

            entity.Property(e => e.PickupPointid).HasColumnName("pickup_pointid");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.HomeNum).HasColumnName("home_num");
            entity.Property(e => e.PostIndex).HasColumnName("post_index");
            entity.Property(e => e.Street).HasColumnName("street");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("products_pkey");

            entity.ToTable("products", "demo");

            entity.HasIndex(e => e.Articul, "products_articul_key").IsUnique();

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Articul).HasColumnName("articul");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Countin).HasColumnName("countin");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Manufacturerid).HasColumnName("manufacturerid");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Sale).HasColumnName("sale");
            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UnitOfMeasurement).HasColumnName("unit_of_measurement");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_categoryid_fkey");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_manufacturerid_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplierid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_supplierid_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("suppliers_pkey");

            entity.ToTable("suppliers", "demo");

            entity.Property(e => e.Supplierid).HasColumnName("supplierid");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
