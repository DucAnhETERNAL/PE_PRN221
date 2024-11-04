using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    DateShip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    UserOrderID = table.Column<int>(type: "int", nullable: false),
                    UserApproveID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipID);
                    table.ForeignKey(
                        name: "FK_Ships_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ships_Users_UserApproveID",
                        column: x => x.UserApproveID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ships_Users_UserOrderID",
                        column: x => x.UserOrderID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Giao Duc" },
                    { 2, "Van Hoc" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Password", "Type", "UserName" },
                values: new object[,]
                {
                    { 1, "adminpass", "Admin", "admin" },
                    { 2, "john123", "User", "john" },
                    { 3, "jane123", "User", "jane" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "BookName", "CategoryID", "Price" },
                values: new object[,]
                {
                    { 1, "Math", 1, 99m },
                    { 2, "Science", 1, 120m },
                    { 3, "Literature", 2, 75m }
                });

            migrationBuilder.InsertData(
                table: "Ships",
                columns: new[] { "ShipID", "BookID", "DateOrder", "DateShip", "IsApproved", "UserApproveID", "UserOrderID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 28, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7743), new DateTime(2024, 11, 4, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7758), true, 1, 2 },
                    { 2, 2, new DateTime(2024, 10, 25, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7762), new DateTime(2024, 11, 2, 9, 25, 25, 537, DateTimeKind.Local).AddTicks(7763), false, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_BookID",
                table: "Ships",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_UserApproveID",
                table: "Ships",
                column: "UserApproveID");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_UserOrderID",
                table: "Ships",
                column: "UserOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
