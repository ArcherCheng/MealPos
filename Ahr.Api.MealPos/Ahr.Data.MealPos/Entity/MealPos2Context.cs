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
        public virtual DbSet<AppKeyValue> AppKeyValue { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppUserLog> AppUserLog { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<MealAddOn> MealAddOn { get; set; }
        public virtual DbSet<MealAddOnRela> MealAddOnRela { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderMaster> OrderMaster { get; set; }

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

            modelBuilder.Entity<AppKeyValue>(entity =>
            {
                entity.Property(e => e.KeyGroup)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.KeyValue)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);
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

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.UserRole).HasMaxLength(15);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

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
                    .HasName("Customer_Idx3");

                entity.HasIndex(e => e.Name)
                    .HasName("Customer_Idx1");

                entity.HasIndex(e => e.TaxId)
                    .HasName("Customer_Idx4");

                entity.HasIndex(e => e.TelNo)
                    .HasName("Customer_Idx2");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.Contactor).HasMaxLength(30);

                entity.Property(e => e.EngName).HasMaxLength(60);

                entity.Property(e => e.FaxNo).HasMaxLength(30);

                entity.Property(e => e.MobileNo).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.PostNo).HasMaxLength(6);

                entity.Property(e => e.TaxId).HasMaxLength(30);

                entity.Property(e => e.TelNo).HasMaxLength(30);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(e => e.BarCode).HasMaxLength(30);

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.MealType)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.Unit)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<MealAddOn>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AddOnName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);
            });

            modelBuilder.Entity<MealAddOnRela>(entity =>
            {
                entity.HasKey(e => new { e.MealId, e.AddOnId })
                    .HasName("MealAddOnRela_Pkey");

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);

                entity.HasOne(d => d.AddOn)
                    .WithMany(p => p.MealAddOnRela)
                    .HasForeignKey(d => d.AddOnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MealAddOnRela_AddOnId");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.MealAddOnRela)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MealAddOnRela_MealId");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

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
                entity.Property(e => e.InvoiceNo).HasMaxLength(30);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.OrderType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SeatNo).HasMaxLength(30);

                entity.Property(e => e.TaxId).HasMaxLength(30);

                entity.Property(e => e.WriteIp).HasMaxLength(30);

                entity.Property(e => e.WriteTime).HasColumnType("datetime");

                entity.Property(e => e.WriteUser).HasMaxLength(30);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderMaster)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("OrderMaster_CustomerId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
