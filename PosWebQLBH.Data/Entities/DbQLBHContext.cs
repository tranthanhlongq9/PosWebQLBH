using System;
using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PosWebQLBH.Data.Configurations;

#nullable disable

namespace PosWebQLBH.Data.Entities
{
    public partial class DbQLBHContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbQLBHContext()
        {
        }

        public DbQLBHContext(DbContextOptions<DbQLBHContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FunctionList> FunctionLists { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SellOrder> SellOrders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }

        public DbSet<Language> Languages { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=.;Database=QuanLyBanHang; UID=sa; PWD=123456;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());

            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__Category__6DB3A68A23DB85E7");

                entity.ToTable("Category");

                entity.HasIndex(e => e.IdCategory, "UQ__Category__6DB3A68B2DE2309C")
                    .IsUnique();

                entity.Property(e => e.IdCategory)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Category");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NameCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Category");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PK__Customer__2D8FDE5FC302B124");

                entity.HasIndex(e => e.IdCustomer, "UQ__Customer__2D8FDE5E1AA3C495")
                    .IsUnique();

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NameCustomer)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Customer");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK__Employee__D9EE4F3660C4DC47");

                entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534CDAC7A07")
                    .IsUnique();

                entity.HasIndex(e => e.IdEmployee, "UQ__Employee__D9EE4F37E4C83A87")
                    .IsUnique();

                entity.Property(e => e.IdEmployee)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Employee");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Role");

                entity.Property(e => e.NameEmployee)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Employee");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Roles");
            });

            modelBuilder.Entity<FunctionList>(entity =>
            {
                entity.HasKey(e => e.IdFunction);

                entity.ToTable("FunctionList");

                entity.Property(e => e.IdFunction)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Function");

                entity.Property(e => e.NameFunction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Function");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.IdHistory)
                    .HasName("PK__History__F51C56DC034F2B20");

                entity.ToTable("History");

                entity.Property(e => e.IdHistory).HasColumnName("ID_History");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.IdEmployee)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Employee");

                entity.Property(e => e.IdProduct)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Product");

                entity.Property(e => e.IdPurchaseOrder).HasColumnName("ID_PurchaseOrder");

                entity.Property(e => e.IdSellOrder).HasColumnName("ID_SellOrder");

                entity.Property(e => e.IdSuppliers)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Suppliers");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceAfterDiscount)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TotalAmount)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.IdInventory)
                    .HasName("PK__Inventor__2210F49E4001C41D");

                entity.ToTable("Inventory");

                entity.Property(e => e.IdInventory).HasColumnName("ID_Inventory");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.IdProduct)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Product");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__Products__522DE496A4887A31");

                entity.HasIndex(e => e.IdProduct, "UQ__Products__522DE497C6210C13")
                    .IsUnique();

                entity.Property(e => e.IdProduct)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Product");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdCategory)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Category");

                entity.Property(e => e.IdUnit)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Unit");

                entity.Property(e => e.ImagePath).HasMaxLength(200);

                entity.Property(e => e.Length).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NameProduct)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Product");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Width).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Category");

                entity.HasOne(d => d.IdUnitNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Unit");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.IdPurchaseOrder)
                    .HasName("PK__Purchase__F1DAA6381B798B2D");

                entity.ToTable("PurchaseOrder");

                entity.HasIndex(e => e.IdPurchaseOrder, "UQ__Purchase__390E8B9067C76FF4")
                    .IsUnique();

                entity.Property(e => e.IdPurchaseOrder).HasColumnName("ID_PurchaseOrder");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdEmployee)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Employee");

                entity.Property(e => e.IdProduct)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Product");

                entity.Property(e => e.IdSupplier)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Supplier");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_Employees");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_Products");

                entity.HasOne(d => d.IdSupplierNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrder_Suppliers");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Roles__43DCD32D3B76F765");

                entity.Property(e => e.IdRole)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Role");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FeatureList)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Feature_List");

                entity.Property(e => e.IdFunction)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Function");

                entity.Property(e => e.NameRole)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Role");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdFunctionNavigation)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.IdFunction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles_FunctionList");
            });

            modelBuilder.Entity<SellOrder>(entity =>
            {
                entity.HasKey(e => e.IdSellOrder)
                    .HasName("PK__SellOrde__9336A44AE17C6FD3");

                entity.ToTable("SellOrder");

                entity.Property(e => e.IdSellOrder).HasColumnName("ID_SellOrder");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdCustomer).HasColumnName("ID_Customer");

                entity.Property(e => e.IdEmployee)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Employee");

                entity.Property(e => e.IdProduct)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Product");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.SellOrders)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellOrder_Customers");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.SellOrders)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellOrder_Employees");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.SellOrders)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SellOrder_Products");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.IdSupplier)
                    .HasName("PK__Supplier__3D9EE7AC0B95603E");

                entity.HasIndex(e => e.IdSupplier, "UQ__Supplier__408B709548A2BC3A")
                    .IsUnique();

                entity.Property(e => e.IdSupplier)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Supplier");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NameSupplier)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Supplier");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Representative)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.IdUnit)
                    .HasName("PK__Unit__EB4517D39276DF6A");

                entity.ToTable("Unit");

                entity.HasIndex(e => e.IdUnit, "UQ__Unit__EB4517D25DF98ADB")
                    .IsUnique();

                entity.Property(e => e.IdUnit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ID_Unit");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.NameUnit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Name_Unit");

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}