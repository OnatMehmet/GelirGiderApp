using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg11AddedFieldToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c1cdd17e-b635-4131-a658-56ac90a775b4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f9578da3-6dcd-4df5-b8fc-b98a5a2f8271"));

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2cc63682-a12b-43f1-88ec-9244e5f1d624"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 7, 13, 38, 21, 199, DateTimeKind.Utc).AddTicks(816) },
                    { new Guid("8d813dea-268f-4613-b622-adc08b95c4f6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 7, 13, 38, 21, 199, DateTimeKind.Utc).AddTicks(821) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2cc63682-a12b-43f1-88ec-9244e5f1d624"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d813dea-268f-4613-b622-adc08b95c4f6"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("c1cdd17e-b635-4131-a658-56ac90a775b4"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 7, 12, 38, 49, 733, DateTimeKind.Utc).AddTicks(6278) },
                    { new Guid("f9578da3-6dcd-4df5-b8fc-b98a5a2f8271"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 7, 12, 38, 49, 733, DateTimeKind.Utc).AddTicks(6287) }
                });
        }
    }
}
