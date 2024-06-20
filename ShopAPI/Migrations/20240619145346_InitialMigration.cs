using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create tables
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order_date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_id = table.Column<string>(type: "TEXT", nullable: false),
                    price_id = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "orders_products",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "INTEGER", nullable: false),
                    product_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders_products", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_orders_products_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Insert data into tables
            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "order_id", "order_date" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1) },
                    { 2, new DateTime(2023, 2, 1) },
                    { 3, new DateTime(2023, 3, 1) },
                    { 4, new DateTime(2023, 4, 1) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "product_id", "name_id", "price_id" },
                values: new object[,]
                {
                    { 1, "Product 1", 10.99m },
                    { 2, "Product 2", 20.49m },
                    { 3, "Product 3", 5.99m },
                    { 4, "Product 4", 15.99m },
                    { 5, "Product 5", 25.99m },
                    { 6, "Product 6", 35.99m },
                    { 7, "Product 7", 45.99m },
                    { 8, "Product 8", 55.99m }
                });

            migrationBuilder.InsertData(
                table: "orders_products",
                columns: new[] { "order_id", "product_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 3 },
                    { 3, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 4, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders_products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
