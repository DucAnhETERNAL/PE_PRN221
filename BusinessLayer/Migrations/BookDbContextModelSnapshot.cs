﻿// <auto-generated />
using System;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessLayer.Migrations
{
    [DbContext(typeof(BookDbContext))]
    partial class BookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessLayer.Books", b =>
                {
                    b.Property<int>("BookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookID"));

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BookID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookID = 1,
                            BookName = "Math",
                            CategoryID = 1,
                            Price = 99m
                        },
                        new
                        {
                            BookID = 2,
                            BookName = "Science",
                            CategoryID = 1,
                            Price = 120m
                        },
                        new
                        {
                            BookID = 3,
                            BookName = "Literature",
                            CategoryID = 2,
                            Price = 75m
                        });
                });

            modelBuilder.Entity("BusinessLayer.Categories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            CategoryName = "Giao Duc"
                        },
                        new
                        {
                            CategoryID = 2,
                            CategoryName = "Van Hoc"
                        });
                });

            modelBuilder.Entity("BusinessLayer.Ships", b =>
                {
                    b.Property<int>("ShipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShipID"));

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOrder")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateShip")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int?>("UserApproveID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("UserOrderID")
                        .HasColumnType("int");

                    b.HasKey("ShipID");

                    b.HasIndex("BookID");

                    b.HasIndex("UserApproveID");

                    b.HasIndex("UserOrderID");

                    b.ToTable("Ships");

                    b.HasData(
                        new
                        {
                            ShipID = 1,
                            BookID = 1,
                            DateOrder = new DateTime(2024, 10, 28, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7743),
                            DateShip = new DateTime(2024, 11, 4, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7758),
                            IsApproved = true,
                            UserApproveID = 1,
                            UserOrderID = 2
                        },
                        new
                        {
                            ShipID = 2,
                            BookID = 2,
                            DateOrder = new DateTime(2024, 10, 25, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7762),
                            DateShip = new DateTime(2024, 11, 2, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7763),
                            IsApproved = false,
                            UserApproveID = 1,
                            UserOrderID = 3
                        });
                });

            modelBuilder.Entity("BusinessLayer.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Password = "adminpass",
                            Type = "Admin",
                            UserName = "admin"
                        },
                        new
                        {
                            UserID = 2,
                            Password = "john123",
                            Type = "User",
                            UserName = "john"
                        },
                        new
                        {
                            UserID = 3,
                            Password = "jane123",
                            Type = "User",
                            UserName = "jane"
                        });
                });

            modelBuilder.Entity("BusinessLayer.Books", b =>
                {
                    b.HasOne("BusinessLayer.Categories", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BusinessLayer.Ships", b =>
                {
                    b.HasOne("BusinessLayer.Books", "Books")
                        .WithMany("Ships")
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessLayer.Users", "UsersApprove")
                        .WithMany()
                        .HasForeignKey("UserApproveID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BusinessLayer.Users", "UsersOrder")
                        .WithMany("Ships")
                        .HasForeignKey("UserOrderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Books");

                    b.Navigation("UsersApprove");

                    b.Navigation("UsersOrder");
                });

            modelBuilder.Entity("BusinessLayer.Books", b =>
                {
                    b.Navigation("Ships");
                });

            modelBuilder.Entity("BusinessLayer.Categories", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BusinessLayer.Users", b =>
                {
                    b.Navigation("Ships");
                });
#pragma warning restore 612, 618
        }
    }
}
