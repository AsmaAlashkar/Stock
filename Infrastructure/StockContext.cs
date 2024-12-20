﻿using System;
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

    public virtual DbSet<CodeFormat> CodeFormats { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemPermission> ItemPermissions { get; set; }

    public virtual DbSet<ItemSupplier> ItemSuppliers { get; set; }

    public virtual DbSet<MainWearhouse> MainWearhouses { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionType> PermissionTypes { get; set; }

    public virtual DbSet<Quantity> Quantities { get; set; }

    public virtual DbSet<SubItem> SubItems { get; set; }

    public virtual DbSet<SubWearhouse> SubWearhouses { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<ViewMainWearhouseWithSubWearhouseHierarchy> ViewMainWearhouseWithSubWearhouseHierarchies { get; set; }

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

        modelBuilder.Entity<CodeFormat>(entity =>
        {
            entity.ToTable("CodeFormat");

            entity.HasIndex(e => e.Format, "UQ_CodeFormats_Format").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Format).HasMaxLength(6);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.ItemId).HasColumnName("Item_ID");
            entity.Property(e => e.CatFk).HasColumnName("Cat_FK");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .HasColumnName("Item_Code");
            entity.Property(e => e.ItemCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Item_Createdat");
            entity.Property(e => e.ItemExperationdate)
                .HasColumnType("datetime")
                .HasColumnName("Item_Experationdate");
            entity.Property(e => e.ItemNameAr)
                .HasMaxLength(50)
                .HasColumnName("Item_NameAr");
            entity.Property(e => e.ItemNameEn)
                .HasMaxLength(50)
                .HasColumnName("Item_NameEn");
            entity.Property(e => e.ItemUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Item_Updatedat");
            entity.Property(e => e.UniteFk).HasColumnName("Unite_FK");

            entity.HasOne(d => d.CatFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.CatFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Category");

            entity.HasOne(d => d.UniteFkNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.UniteFk)
                .HasConstraintName("FK_Items_Unite");
        });

        modelBuilder.Entity<ItemPermission>(entity =>
        {
            entity.HasKey(e => e.ItemPerId).HasName("PK_SubItemPermission");

            entity.ToTable("ItemPermission");

            entity.Property(e => e.ItemPerId).HasColumnName("ItemPer_ID");
            entity.Property(e => e.ItemFk).HasColumnName("Item_FK");
            entity.Property(e => e.PermFk).HasColumnName("Perm_FK");

            entity.HasOne(d => d.ItemFkNavigation).WithMany(p => p.ItemPermissions)
                .HasForeignKey(d => d.ItemFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemPermission_Items");

            entity.HasOne(d => d.PermFkNavigation).WithMany(p => p.ItemPermissions)
                .HasForeignKey(d => d.PermFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemPermission_Permission");
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
            entity.Property(e => e.MainDescriptionAr).HasColumnName("Main_DescriptionAr");
            entity.Property(e => e.MainDescriptionEn).HasColumnName("Main_DescriptionEn");
            entity.Property(e => e.MainNameAr)
                .HasMaxLength(50)
                .HasColumnName("Main_NameAr");
            entity.Property(e => e.MainNameEn)
                .HasMaxLength(50)
                .HasColumnName("Main_NameEn");
            entity.Property(e => e.MainUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Updatedat");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermId);

            entity.ToTable("Permission");

            entity.HasIndex(e => e.PermCode, "UQ_PermCode").IsUnique();

            entity.Property(e => e.PermId).HasColumnName("Perm_ID");
            entity.Property(e => e.PermCode).HasMaxLength(10);
            entity.Property(e => e.PermCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Perm_Createdat");
            entity.Property(e => e.PermTypeFk).HasColumnName("PermType_FK");
            entity.Property(e => e.SubFk).HasColumnName("Sub_FK");

            entity.HasOne(d => d.DestinationSubFkNavigation).WithMany(p => p.PermissionDestinationSubFkNavigations)
                .HasForeignKey(d => d.DestinationSubFk)
                .HasConstraintName("FK_Permission_SubWearhouse");

            entity.HasOne(d => d.PermTypeFkNavigation).WithMany(p => p.Permissions)
                .HasForeignKey(d => d.PermTypeFk)
                .HasConstraintName("FK_Permission_PermissionType");

            entity.HasOne(d => d.SubFkNavigation).WithMany(p => p.PermissionSubFkNavigations)
                .HasForeignKey(d => d.SubFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_SubWearhouse1");
        });

        modelBuilder.Entity<PermissionType>(entity =>
        {
            entity.HasKey(e => e.PerId);

            entity.ToTable("PermissionType");

            entity.Property(e => e.PerId).HasColumnName("Per_ID");
            entity.Property(e => e.PerTypeValueAr).HasMaxLength(50);
            entity.Property(e => e.PerTypeValueEn).HasMaxLength(50);
        });

        modelBuilder.Entity<Quantity>(entity =>
        {
            entity.HasKey(e => e.QuantityId).HasName("PK_Quantity_1");

            entity.ToTable("Quantity");

            entity.Property(e => e.QuantityId).HasColumnName("Quantity_ID");
            entity.Property(e => e.ItemFk).HasColumnName("Item_FK");
            entity.Property(e => e.QuantityCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Quantity_Createdat");
            entity.Property(e => e.QuantityUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Quantity_Updatedat");

            entity.HasOne(d => d.ItemFkNavigation).WithMany(p => p.Quantities)
                .HasForeignKey(d => d.ItemFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Items");
        });

        modelBuilder.Entity<SubItem>(entity =>
        {
            entity.ToTable("SubItem");

            entity.Property(e => e.ItemFk).HasColumnName("Item_FK");
            entity.Property(e => e.SubFk).HasColumnName("Sub_FK");

            entity.HasOne(d => d.ItemFkNavigation).WithMany(p => p.SubItems)
                .HasForeignKey(d => d.ItemFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubItem_Items");

            entity.HasOne(d => d.SubFkNavigation).WithMany(p => p.SubItems)
                .HasForeignKey(d => d.SubFk)
                .HasConstraintName("FK_SubItem_SubWearhouse");
        });

        modelBuilder.Entity<SubWearhouse>(entity =>
        {
            entity.HasKey(e => e.SubId);

            entity.ToTable("SubWearhouse");

            entity.HasIndex(e => new { e.ParentSubWearhouseId, e.Delet }, "idx_SubWearhouse_ParentDelet");

            entity.Property(e => e.SubId).HasColumnName("Sub_ID");
            entity.Property(e => e.MainFk).HasColumnName("Main_FK");
            entity.Property(e => e.SubAddressAr).HasColumnName("Sub_AddressAr");
            entity.Property(e => e.SubAddressEn).HasColumnName("Sub_AddressEn");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubDescriptionAr).HasColumnName("Sub_DescriptionAr");
            entity.Property(e => e.SubDescriptionEn).HasColumnName("Sub_DescriptionEn");
            entity.Property(e => e.SubNameAr)
                .HasMaxLength(50)
                .HasColumnName("Sub_NameAr");
            entity.Property(e => e.SubNameEn)
                .HasMaxLength(50)
                .HasColumnName("Sub_NameEn");
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
            entity.Property(e => e.UnitDescAr).HasColumnName("Unit_DescAr");
            entity.Property(e => e.UnitDescEn).HasColumnName("Unit_DescEn");
            entity.Property(e => e.UnitNameAr)
                .HasMaxLength(50)
                .HasColumnName("Unit_NameAr");
            entity.Property(e => e.UnitNameEn)
                .HasMaxLength(50)
                .HasColumnName("Unit_NameEn");
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
            entity.Property(e => e.ItemNameAr)
                .HasMaxLength(50)
                .HasColumnName("Item_NameAr");
            entity.Property(e => e.ItemNameEn)
                .HasMaxLength(50)
                .HasColumnName("Item_NameEn");
            entity.Property(e => e.MainAdderess).HasColumnName("Main_Adderess");
            entity.Property(e => e.MainCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Createdat");
            entity.Property(e => e.MainDescriptionAr).HasColumnName("Main_DescriptionAr");
            entity.Property(e => e.MainDescriptionEn).HasColumnName("Main_DescriptionEn");
            entity.Property(e => e.MainId).HasColumnName("Main_ID");
            entity.Property(e => e.MainNameAr)
                .HasMaxLength(50)
                .HasColumnName("Main_NameAr");
            entity.Property(e => e.MainNameEn)
                .HasMaxLength(50)
                .HasColumnName("Main_NameEn");
            entity.Property(e => e.MainUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Main_Updatedat");
            entity.Property(e => e.Md).HasColumnName("MD");
            entity.Property(e => e.Sd).HasColumnName("SD");
            entity.Property(e => e.SubAddressAr).HasColumnName("Sub_AddressAr");
            entity.Property(e => e.SubAddressEn).HasColumnName("Sub_AddressEn");
            entity.Property(e => e.SubCreatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Createdat");
            entity.Property(e => e.SubDescriptionAr).HasColumnName("Sub_DescriptionAr");
            entity.Property(e => e.SubDescriptionEn).HasColumnName("Sub_DescriptionEn");
            entity.Property(e => e.SubId).HasColumnName("Sub_ID");
            entity.Property(e => e.SubNameAr)
                .HasMaxLength(50)
                .HasColumnName("Sub_NameAr");
            entity.Property(e => e.SubNameEn)
                .HasMaxLength(50)
                .HasColumnName("Sub_NameEn");
            entity.Property(e => e.SubUpdatedat)
                .HasColumnType("datetime")
                .HasColumnName("Sub_Updatedat");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
