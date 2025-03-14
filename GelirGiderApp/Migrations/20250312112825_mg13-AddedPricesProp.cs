using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg13AddedPricesProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("093cb419-2118-4ef8-8187-cd53fb4821a9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a7eb1b59-c7d5-44b8-8dd3-c3ddbe04f46c"));

            migrationBuilder.AddColumn<decimal>(
                name: "AnalysisPurchasePrice",
                table: "ProductTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPurchasePrice",
                table: "ProductTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0ecea1bf-7b92-4d3d-9fbc-dde7e6f1febe"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 12, 11, 28, 24, 861, DateTimeKind.Utc).AddTicks(9056) },
                    { new Guid("7b803751-6727-424a-b0b5-7cc7f5284bbd"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 12, 11, 28, 24, 861, DateTimeKind.Utc).AddTicks(9060) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0ecea1bf-7b92-4d3d-9fbc-dde7e6f1febe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7b803751-6727-424a-b0b5-7cc7f5284bbd"));

            migrationBuilder.DropColumn(
                name: "AnalysisPurchasePrice",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "MonthlyPurchasePrice",
                table: "ProductTypes");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("093cb419-2118-4ef8-8187-cd53fb4821a9"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 11, 14, 2, 35, 360, DateTimeKind.Utc).AddTicks(7673) },
                    { new Guid("a7eb1b59-c7d5-44b8-8dd3-c3ddbe04f46c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 11, 14, 2, 35, 360, DateTimeKind.Utc).AddTicks(7669) }
                });
        }
    }
}
