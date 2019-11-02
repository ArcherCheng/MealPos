using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ahr.Data.MealPos
{
    public partial class MealPos2Context : DbContext
    {
        public MealPos2Context()
        {
        }

        public MealPos2Context(DbContextOptions<MealPos2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AppDataLog> AppDataLog { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppUserLog> AppUserLog { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<MealAddOn> MealAddOn { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderMaster> OrderMaster { get; set; }
        public virtual DbSet<SysCode> SysCode { get; set; }
        public virtual DbSet<Vender> Vender { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=.\\sqlExpress;database=MealPos2;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppDataLog>(entity =>
            {
                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.WriteTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("AppUser_Idx1")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("AppUser_Idx2")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("AppUser_Idx3")
                    .IsUnique();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.IsInWork)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LoginDate).HasColumnType("datetime");

                entity.Property(e => e.MainPhotoUrl).HasMaxLength(250);

                entity.Property(e => e.PasswordHash).HasMaxLength(2000);

                entity.Property(e => e.PasswordSalt).HasMaxLength(2000);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UserRole).HasMaxLength(15);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<AppUserLog>(entity =>
            {
                entity.Property(e => e.Destination).HasMaxLength(255);

                entity.Property(e => e.IpAddress).HasMaxLength(30);

                entity.Property(e => e.Method).HasMaxLength(30);

                entity.Property(e => e.QueryString).HasMaxLength(255);

                entity.Property(e => e.Refer).HasMaxLength(255);

                entity.Property(e => e.RequestTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.MobileNo)
                    .HasName("Customer_Idx3")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("Customer_Idx1")
                    .IsUnique();

                entity.HasIndex(e => e.TaxNo)
                    .HasName("Customer_Idx4")
                    .IsUnique();

                entity.HasIndex(e => e.TelNo)
                    .HasName("Customer_Idx2")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.EngName).HasMaxLength(60);

                entity.Property(e => e.FaxNo).HasMaxLength(30);

                entity.Property(e => e.MobileNo).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.Operator).HasMaxLength(30);

                entity.Property(e => e.PostNo).HasMaxLength(6);

                entity.Property(e => e.TaxNo).HasMaxLength(30);

                entity.Property(e => e.TelNo).HasMaxLength(30);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(e => e.BarCode).HasMaxLength(30);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<MealAddOn>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Descriptions)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.MealAddOn)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MealAddOn_MealId");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderDetail_MasterId");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderDetail_MealId");
            });

            modelBuilder.Entity<OrderMaster>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerTaxNo).HasMaxLength(30);

                entity.Property(e => e.InvoiceNo).HasMaxLength(30);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.Operator).HasMaxLength(30);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SeatNo).HasMaxLength(30);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderMaster)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("OrderMaster_CustomerId");
            });

            modelBuilder.Entity<SysCode>(entity =>
            {
                entity.Property(e => e.CodeGroup)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CodeValue)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<Vender>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.EngName).HasMaxLength(120);

                entity.Property(e => e.FaxNo).HasMaxLength(30);

                entity.Property(e => e.MobileNo).HasMaxLength(30);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Operator).HasMaxLength(30);

                entity.Property(e => e.PostNo).HasMaxLength(6);

                entity.Property(e => e.TaxId).HasMaxLength(10);

                entity.Property(e => e.TelNo).HasMaxLength(30);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.VenderName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
