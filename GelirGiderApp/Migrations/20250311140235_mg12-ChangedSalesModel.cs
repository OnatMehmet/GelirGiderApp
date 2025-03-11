using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg12ChangedSalesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2cc63682-a12b-43f1-88ec-9244e5f1d624"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d813dea-268f-4613-b622-adc08b95c4f6"));

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("093cb419-2118-4ef8-8187-cd53fb4821a9"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 11, 14, 2, 35, 360, DateTimeKind.Utc).AddTicks(7673) },
                    { new Guid("a7eb1b59-c7d5-44b8-8dd3-c3ddbe04f46c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 11, 14, 2, 35, 360, DateTimeKind.Utc).AddTicks(7669) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("093cb419-2118-4ef8-8187-cd53fb4821a9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a7eb1b59-c7d5-44b8-8dd3-c3ddbe04f46c"));

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Sales");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2cc63682-a12b-43f1-88ec-9244e5f1d624"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 7, 13, 38, 21, 199, DateTimeKind.Utc).AddTicks(816) },
                    { new Guid("8d813dea-268f-4613-b622-adc08b95c4f6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 7, 13, 38, 21, 199, DateTimeKind.Utc).AddTicks(821) }
                });
        }
    }
}
