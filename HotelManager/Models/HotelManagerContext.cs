using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace HotelManager.Models
{
    public partial class HotelManagerContext : DbContext
    {
        public HotelManagerContext()
        {
        }

        public HotelManagerContext(DbContextOptions<HotelManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hotel> Hotels { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderRoom> OrderRooms { get; set; } = null!;
        public virtual DbSet<OrderService> OrderServices { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Recharge> Recharges { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conf.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("Hotel");

                entity.Property(e => e.HotelName).HasColumnName("Hotel_Name");

                entity.Property(e => e.PId).HasColumnName("P_Id");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK_Hotel_Province");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.DateBook)
                    .HasColumnType("date")
                    .HasColumnName("Date_Book");

                entity.Property(e => e.DateCheckout)
                    .HasColumnType("date")
                    .HasColumnName("Date_Checkout");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderRoom>(entity =>
            {
                entity.Property(e => e.OId).HasColumnName("O_Id");

                entity.Property(e => e.RId).HasColumnName("R_Id");

                entity.HasOne(d => d.OIdNavigation)
                    .WithMany(p => p.OrderRooms)
                    .HasForeignKey(d => d.OId)
                    .HasConstraintName("FK_OrderRooms_Order");

                entity.HasOne(d => d.RIdNavigation)
                    .WithMany(p => p.OrderRooms)
                    .HasForeignKey(d => d.RId)
                    .HasConstraintName("FK_OrderRooms_Room");
            });

            modelBuilder.Entity<OrderService>(entity =>
            {
                entity.ToTable("OrderService");

                entity.Property(e => e.OId).HasColumnName("O_Id");

                entity.Property(e => e.SId).HasColumnName("S_Id");

                entity.HasOne(d => d.OIdNavigation)
                    .WithMany(p => p.OrderServices)
                    .HasForeignKey(d => d.OId)
                    .HasConstraintName("FK_OrderService_Order");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.OrderServices)
                    .HasForeignKey(d => d.SId)
                    .HasConstraintName("FK_OrderService_Service1");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.ProvinceName).HasColumnName("Province_Name");
            });

            modelBuilder.Entity<Recharge>(entity =>
            {
                entity.ToTable("Recharge");

                entity.Property(e => e.BankId).HasColumnName("Bank_Id");

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .HasColumnName("Bank_Name");

                entity.Property(e => e.DepositCode).HasColumnName("Deposit_Code");

                entity.Property(e => e.RechargeValue)
                    .HasColumnType("money")
                    .HasColumnName("Recharge_Value");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Recharges)
                    .HasForeignKey(d => d.UId)
                    .HasConstraintName("FK_Recharge_User");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.HId).HasColumnName("H_Id");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RoomName).HasColumnName("Room_Name");

                entity.Property(e => e.RoomType).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.HIdNavigation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Hotel");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.HId).HasColumnName("H_Id");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ServiceName).HasColumnName("Service_Name");

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.HIdNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.HId)
                    .HasConstraintName("FK_Service_Hotel");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Money).HasColumnType("money");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
