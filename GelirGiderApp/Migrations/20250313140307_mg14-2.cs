using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg142 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientProduct_ProductTypes_ProductTypeId",
                table: "PatientProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientProduct_Products_ProductId",
                table: "PatientProduct");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0fc46264-898e-43fe-8d81-08fe90a0da8e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("4f6993da-06eb-44e6-9811-59d185ba2e08"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 13, 14, 3, 6, 574, DateTimeKind.Utc).AddTicks(6856) });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProduct_ProductTypes_ProductTypeId",
                table: "PatientProduct",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProduct_Products_ProductId",
                table: "PatientProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientProduct_ProductTypes_ProductTypeId",
                table: "PatientProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientProduct_Products_ProductId",
                table: "PatientProduct");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4f6993da-06eb-44e6-9811-59d185ba2e08"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("0fc46264-898e-43fe-8d81-08fe90a0da8e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 13, 13, 55, 52, 227, DateTimeKind.Utc).AddTicks(37) });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProduct_ProductTypes_ProductTypeId",
                table: "PatientProduct",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientProduct_Products_ProductId",
                table: "PatientProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
