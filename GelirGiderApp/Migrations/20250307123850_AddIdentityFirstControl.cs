using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityFirstControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2bd616f4-d52d-4197-af1c-448704bcf62b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8591a9d6-ed63-4e9b-97be-7db17d8924e7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("c1cdd17e-b635-4131-a658-56ac90a775b4"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 7, 12, 38, 49, 733, DateTimeKind.Utc).AddTicks(6278) },
                    { new Guid("f9578da3-6dcd-4df5-b8fc-b98a5a2f8271"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 7, 12, 38, 49, 733, DateTimeKind.Utc).AddTicks(6287) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c1cdd17e-b635-4131-a658-56ac90a775b4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f9578da3-6dcd-4df5-b8fc-b98a5a2f8271"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("2bd616f4-d52d-4197-af1c-448704bcf62b"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", null, new DateTime(2025, 3, 7, 9, 22, 25, 188, DateTimeKind.Utc).AddTicks(9977) },
                    { new Guid("8591a9d6-ed63-4e9b-97be-7db17d8924e7"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", null, new DateTime(2025, 3, 7, 9, 22, 25, 188, DateTimeKind.Utc).AddTicks(9981) }
                });
        }
    }
}
