using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg8AddedUsageStageToSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("555d17ad-bd0e-4e83-aa39-5792c8bc9396"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7d64c84e-ebbb-4063-b853-099542dbe358"));

            migrationBuilder.AddColumn<int>(
                name: "UsageStage",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("385e83ec-77d1-44e5-acfd-1af02287fbc3"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 4, 13, 26, 43, 142, DateTimeKind.Utc).AddTicks(850) },
                    { new Guid("b71f6663-82e6-49c4-b3cd-01e15000187c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 4, 13, 26, 43, 142, DateTimeKind.Utc).AddTicks(881) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_Code",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("385e83ec-77d1-44e5-acfd-1af02287fbc3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b71f6663-82e6-49c4-b3cd-01e15000187c"));

            migrationBuilder.DropColumn(
                name: "UsageStage",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("555d17ad-bd0e-4e83-aa39-5792c8bc9396"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 4, 10, 52, 32, 14, DateTimeKind.Utc).AddTicks(1685) },
                    { new Guid("7d64c84e-ebbb-4063-b853-099542dbe358"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 4, 10, 52, 32, 14, DateTimeKind.Utc).AddTicks(1711) }
                });
        }
    }
}
