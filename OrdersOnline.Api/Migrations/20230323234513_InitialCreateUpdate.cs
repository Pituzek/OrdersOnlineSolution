using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrdersOnline.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "AdditionalInfo", "ClientName", "CreateDate", "OrderPrice", "Status" },
                values: new object[,]
                {
                    { 1, "N/A", "John Smith", new DateTime(2023, 3, 23, 0, 45, 13, 510, DateTimeKind.Local).AddTicks(2765), 50.00m, 0 },
                    { 2, "Will be cancelled due to product unavailability", "Jane Doe", new DateTime(2023, 3, 22, 0, 45, 13, 510, DateTimeKind.Local).AddTicks(2861), 75.00m, 0 },
                    { 3, "Will be delivered to front desk", "Bob Johnson", new DateTime(2023, 3, 21, 0, 45, 13, 510, DateTimeKind.Local).AddTicks(2865), 100.00m, 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderLines",
                columns: new[] { "OrderLineId", "OrderId", "Price", "Product" },
                values: new object[,]
                {
                    { 1, 1, 25.00m, "Product A" },
                    { 2, 1, 25.00m, "Product B" },
                    { 3, 2, 50.00m, "Product C" },
                    { 4, 2, 25.00m, "Product D" },
                    { 5, 3, 50.00m, "Product E" },
                    { 6, 3, 50.00m, "Product F" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderLines",
                keyColumn: "OrderLineId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
