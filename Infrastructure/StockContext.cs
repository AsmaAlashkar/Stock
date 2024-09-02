using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Standard.Entities;

public partial class StockContext : DbContext
{
    public StockContext()
    {
    }

    public StockContext(DbContextOptions<StockContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemSupplier> ItemSuppliers { get; set; }

    public virtual DbSet<MainWearhouse> MainWearhouses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<SubWearhouse> SubWearhouses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-F9A72EH;Database=Stock;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.ToTable("Category");

            entity.Property(e => e.CatId)
                .ValueGeneratedNever()
                .HasColumnName("Cat_ID");
            entity.Property(e => e.CatName)
                .HasMaxLength(50)
                .HasColumnName("Cat_Name");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("Item_ID");
            entity.Property(e => e.CatFk).HasColumnName("Cat_FK");
            entity.Property(e => e.ItemCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Item_Createdat");
            entity.Property(e => e.ItemExperationdate)
                .HasColumnType("datetime")
                .HasColumnName("Item_Experationdate");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("Item_Name");
            entity.Property(e => e.ItemUnit)
                .HasMaxLength(50)
                .HasColumnName("Item_Unit");
            entity.Property(e => e.ItemUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Item_Updatedat");
            entity.Property(e => e.SubFk).HasColumnName("Sub_FK");

            entity.HasOne(d => d.CatFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.CatFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Category");

            entity.HasOne(d => d.SubFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.SubFk)
                .HasConstraintName("FK_Items_SubWearhouse");
        });

        modelBuilder.Entity<ItemSupplier>(entity =>
        {
            entity.HasKey(e => e.ItemSuppliersId);

            entity.Property(e => e.ItemSuppliersId)
                .ValueGeneratedNever()
                .HasColumnName("ItemSuppliers_ID");
            entity.Property(e => e.ItemsFk).HasColumnName("Items_FK");
            entity.Property(e => e.SuppliersFk).HasColumnName("Suppliers_Fk");

            entity.HasOne(d => d.ItemsFkNavigation).WithMany(p => p.ItemSuppliers)
                .HasForeignKey(d => d.ItemsFk)
                .HasConstraintName("FK_ItemSuppliers_Items");

            entity.HasOne(d => d.SuppliersFkNavigation).WithMany(p => p.ItemSuppliers)
                .HasForeignKey(d => d.SuppliersFk)
                .HasConstraintName("FK_ItemSuppliers_Suppliers");
        });

        modelBuilder.Entity<MainWearhouse>(entity =>
        {
            entity.HasKey(e => e.MainId);

            entity.ToTable("MainWearhouse");

            entity.Property(e => e.MainId)
                .ValueGeneratedNever()
                .HasColumnName("Main_ID");
            entity.Property(e => e.MainAdderess)
                .HasColumnType("text")
                .HasColumnName("Main_Adderess");
            entity.Property(e => e.MainCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Createdat");
            entity.Property(e => e.MainDescription)
                .HasMaxLength(50)
                .HasColumnName("Main_Description");
            entity.Property(e => e.MainName)
                .HasMaxLength(50)
                .HasColumnName("Main_Name");
            entity.Property(e => e.MainUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Updatedat");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.StockId)
                .ValueGeneratedNever()
                .HasColumnName("Stock_ID");
            entity.Property(e => e.ItemFk).HasColumnName("Item_FK");
            entity.Property(e => e.StockCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Stock_Createdat");
            entity.Property(e => e.StockUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Stock_Updatedat");

            entity.HasOne(d => d.ItemFkNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ItemFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Items");
        });

        modelBuilder.Entity<SubWearhouse>(entity =>
        {
            entity.HasKey(e => e.SubId);

            entity.ToTable("SubWearhouse");

            entity.Property(e => e.SubId)
                .ValueGeneratedNever()
                .HasColumnName("Sub_ID");
            entity.Property(e => e.MainFk).HasColumnName("Main_FK");
            entity.Property(e => e.SubAddress).HasColumnName("Sub_Address");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubDescription).HasColumnName("Sub_Description");
            entity.Property(e => e.SubUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Updatedat");

            entity.HasOne(d => d.MainFkNavigation).WithMany(p => p.SubWearhouses)
                .HasForeignKey(d => d.MainFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubWearhouse_MainWearhouse");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId)
                .ValueGeneratedNever()
                .HasColumnName("Supplier_ID");
            entity.Property(e => e.ContactPeraon).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.SupCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sup_Createdat");
            entity.Property(e => e.SupUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sup_Updatedat");
            entity.Property(e => e.SuppliarAddreaa)
                .HasColumnType("text")
                .HasColumnName("Suppliar_Addreaa");
            entity.Property(e => e.SuppliersName)
                .HasMaxLength(50)
                .HasColumnName("Suppliers_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
