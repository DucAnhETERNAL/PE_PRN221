using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BusinessLayer
{
    public class BookDbContext : DbContext
    {
        public BookDbContext() 
        {
        }
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Ships> Ships { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình các mối quan hệ
            modelBuilder.Entity<Ships>()
                .HasOne(s => s.UsersOrder)
                .WithMany(u => u.Ships)
                .HasForeignKey(s => s.UserOrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ships>()
                .HasOne(s => s.UsersApprove)
                .WithMany()
                .HasForeignKey(s => s.UserApproveID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Books>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID);

            // Dữ liệu mẫu cho bảng Categories
            modelBuilder.Entity<Categories>().HasData(
                new Categories { CategoryID = 1, CategoryName = "Giao Duc" },
                new Categories { CategoryID = 2, CategoryName = "Van Hoc" }
            );

            // Dữ liệu mẫu cho bảng Books
            modelBuilder.Entity<Books>().HasData(
                new Books { BookID = 1, BookName = "Math", CategoryID = 1, Price = 99 },
                new Books { BookID = 2, BookName = "Science", CategoryID = 1, Price = 120 },
                new Books { BookID = 3, BookName = "Literature", CategoryID = 2, Price = 75 }
            );

            // Dữ liệu mẫu cho bảng Users
            modelBuilder.Entity<Users>().HasData(
                new Users { UserID = 1, UserName = "admin", Password = "adminpass", Type = "Admin" },
                new Users { UserID = 2, UserName = "john", Password = "john123", Type = "User" },
                new Users { UserID = 3, UserName = "jane", Password = "jane123", Type = "User" }
            );

            // Dữ liệu mẫu cho bảng Ships với thuộc tính IsApproved
            modelBuilder.Entity<Ships>().HasData(
                new Ships
                {
                    ShipID = 1,
                    DateOrder = DateTime.Now.AddDays(-7),
                    DateShip = DateTime.Now,
                    BookID = 1,
                    UserOrderID = 2,
                    UserApproveID = 1,
                    IsApproved = true // Phê duyệt
                },
                new Ships
                {
                    ShipID = 2,
                    DateOrder = DateTime.Now.AddDays(-10),
                    DateShip = DateTime.Now.AddDays(-2),
                    BookID = 2,
                    UserOrderID = 3,
                    UserApproveID = 1,
                    IsApproved = false // Chưa phê duyệt
                }
            );
        }
    }
}
