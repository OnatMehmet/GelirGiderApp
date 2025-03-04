using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg6DiagnosesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2573525e-2964-469d-8a38-8172574d270d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b2b636aa-ed66-4b59-b83b-9735a46aec0e"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("41081180-e6f5-48ac-9460-d0a853e2880a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 4, 8, 20, 28, 621, DateTimeKind.Utc).AddTicks(3291) },
                    { new Guid("eb49f5df-c468-4569-a297-3f52a0517048"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 4, 8, 20, 28, 621, DateTimeKind.Utc).AddTicks(3345) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("41081180-e6f5-48ac-9460-d0a853e2880a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eb49f5df-c468-4569-a297-3f52a0517048"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Diagnoses");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Diagnoses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2573525e-2964-469d-8a38-8172574d270d"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 4, 7, 48, 27, 970, DateTimeKind.Utc).AddTicks(3593) },
                    { new Guid("b2b636aa-ed66-4b59-b83b-9735a46aec0e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 4, 7, 48, 27, 970, DateTimeKind.Utc).AddTicks(3557) }
                });
        }
    }
}
