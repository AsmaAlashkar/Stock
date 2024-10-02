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

    public virtual DbSet<CategoriesHirarichy> CategoriesHirarichies { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemSupplier> ItemSuppliers { get; set; }

    public virtual DbSet<MainWearhouse> MainWearhouses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<SubWearhouse> SubWearhouses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<ViewMainWearhouseWithSubWearhouseHierarchy> ViewMainWearhouseWithSubWearhouseHierarchies { get; set; }

    public virtual DbSet<ViewWearhouseItem> ViewWearhouseItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Stock;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriesHirarichy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CategoriesHirarichy");

            entity.Property(e => e.CatDesAr).HasColumnName("Cat_DesAr");
            entity.Property(e => e.CatDesEn).HasColumnName("Cat_DesEn");
            entity.Property(e => e.CatId).HasColumnName("Cat_ID");
            entity.Property(e => e.CatNameAr)
                .HasMaxLength(50)
                .HasColumnName("Cat_NameAr");
            entity.Property(e => e.CatNameEn)
                .HasMaxLength(50)
                .HasColumnName("Cat_NameEn");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.ToTable("Category");

            entity.Property(e => e.CatId).HasColumnName("Cat_ID");
            entity.Property(e => e.CatDesAr).HasColumnName("Cat_DesAr");
            entity.Property(e => e.CatDesEn).HasColumnName("Cat_DesEn");
            entity.Property(e => e.CatNameAr)
                .HasMaxLength(50)
                .HasColumnName("Cat_NameAr");
            entity.Property(e => e.CatNameEn)
                .HasMaxLength(50)
                .HasColumnName("Cat_NameEn");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Category_Category");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
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
            entity.Property(e => e.ItemUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Item_Updatedat");
            entity.Property(e => e.SubFk).HasColumnName("Sub_FK");
            entity.Property(e => e.UniteFk).HasColumnName("Unite_FK");

            entity.HasOne(d => d.CatFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.CatFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Category");

            entity.HasOne(d => d.SubFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.SubFk)
                .HasConstraintName("FK_Items_SubWearhouse");

            entity.HasOne(d => d.UniteFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.UniteFk)
                .HasConstraintName("FK_Items_Unite");
        });

        modelBuilder.Entity<ItemSupplier>(entity =>
        {
            entity.HasKey(e => e.ItemSuppliersId);

            entity.Property(e => e.ItemSuppliersId).HasColumnName("ItemSuppliers_ID");
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

            entity.Property(e => e.MainId).HasColumnName("Main_ID");
            entity.Property(e => e.MainAdderess).HasColumnName("Main_Adderess");
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

            entity.Property(e => e.StockId).HasColumnName("Stock_ID");
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

            entity.HasIndex(e => new { e.ParentSubWearhouseId, e.Delet }, "idx_SubWearhouse_ParentDelet");

            entity.Property(e => e.SubId).HasColumnName("Sub_ID");
            entity.Property(e => e.MainFk).HasColumnName("Main_FK");
            entity.Property(e => e.SubAddress).HasColumnName("Sub_Address");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubDescription).HasColumnName("Sub_Description");
            entity.Property(e => e.SubName)
                .HasMaxLength(50)
                .HasColumnName("Sub_Name");
            entity.Property(e => e.SubUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Updatedat");

            entity.HasOne(d => d.MainFkNavigation).WithMany(p => p.SubWearhouses)
                .HasForeignKey(d => d.MainFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubWearhouse_MainWearhouse");

            entity.HasOne(d => d.ParentSubWearhouse).WithMany(p => p.InverseParentSubWearhouse)
                .HasForeignKey(d => d.ParentSubWearhouseId)
                .HasConstraintName("FK_SubWearhouse_SubWearhouse");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_ID");
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
            entity.Property(e => e.SuppliarAddreaa).HasColumnName("Suppliar_Addreaa");
            entity.Property(e => e.SuppliersName)
                .HasMaxLength(50)
                .HasColumnName("Suppliers_Name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK_Unite");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitId).HasColumnName("Unit_ID");
            entity.Property(e => e.UnitCreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Unit_CreatedAt");
            entity.Property(e => e.UnitDesc).HasColumnName("Unit_Desc");
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .HasColumnName("Unit_Name");
            entity.Property(e => e.UnitUpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Unit_UpdatedAt");
        });

        modelBuilder.Entity<ViewMainWearhouseWithSubWearhouseHierarchy>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_MainWearhouseWithSubWearhouseHierarchy");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ItemExperationdate)
                .HasColumnType("datetime")
                .HasColumnName("Item_Experationdate");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("Item_Name");
            entity.Property(e => e.MainAdderess).HasColumnName("Main_Adderess");
            entity.Property(e => e.MainCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Createdat");
            entity.Property(e => e.MainDescription)
                .HasMaxLength(50)
                .HasColumnName("Main_Description");
            entity.Property(e => e.MainId).HasColumnName("Main_ID");
            entity.Property(e => e.MainName)
                .HasMaxLength(50)
                .HasColumnName("Main_Name");
            entity.Property(e => e.MainUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Updatedat");
            entity.Property(e => e.Md).HasColumnName("MD");
            entity.Property(e => e.Sd).HasColumnName("SD");
            entity.Property(e => e.SubAddress).HasColumnName("Sub_Address");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubDescription).HasColumnName("Sub_Description");
            entity.Property(e => e.SubId).HasColumnName("Sub_ID");
            entity.Property(e => e.SubName)
                .HasMaxLength(50)
                .HasColumnName("Sub_Name");
            entity.Property(e => e.SubUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Updatedat");
        });

        modelBuilder.Entity<ViewWearhouseItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_WearhouseItem");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ItemExperationdate)
                .HasColumnType("datetime")
                .HasColumnName("Item_Experationdate");
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("Item_Name");
            entity.Property(e => e.MainAdderess).HasColumnName("Main_Adderess");
            entity.Property(e => e.MainCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Createdat");
            entity.Property(e => e.MainDescription)
                .HasMaxLength(50)
                .HasColumnName("Main_Description");
            entity.Property(e => e.MainId).HasColumnName("Main_ID");
            entity.Property(e => e.MainName)
                .HasMaxLength(50)
                .HasColumnName("Main_Name");
            entity.Property(e => e.MainUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Updatedat");
            entity.Property(e => e.Md).HasColumnName("MD");
            entity.Property(e => e.Sd).HasColumnName("SD");
            entity.Property(e => e.SubAddress).HasColumnName("Sub_Address");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubId).HasColumnName("Sub_ID");
            entity.Property(e => e.SubName)
                .HasMaxLength(50)
                .HasColumnName("Sub_Name");
            entity.Property(e => e.SubUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Updatedat");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
