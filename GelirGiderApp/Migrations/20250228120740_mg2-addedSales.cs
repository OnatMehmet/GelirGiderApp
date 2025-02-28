using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GelirGiderApp.Migrations
{
    /// <inheritdoc />
    public partial class mg2addedSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PatientProducts");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0b15a63e-21dc-4115-a79b-3697ed2be271"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("459ed664-167e-49d9-96bb-464d36fb5899"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("a9f1c7f0-8601-444c-840d-8a745b8b273e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 28, 15, 7, 39, 352, DateTimeKind.Local).AddTicks(7494) },
                    { new Guid("c71408fa-caa7-4ac4-935b-c15d5f830355"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 28, 15, 7, 39, 352, DateTimeKind.Local).AddTicks(7527) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a9f1c7f0-8601-444c-840d-8a745b8b273e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c71408fa-caa7-4ac4-935b-c15d5f830355"));

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "PatientProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientProducts_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_PatientProducts_PatientProductId",
                        column: x => x.PatientProductId,
                        principalTable: "PatientProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsActive", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0b15a63e-21dc-4115-a79b-3697ed2be271"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Doctor", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 27, 14, 31, 2, 15, DateTimeKind.Local).AddTicks(2315) },
                    { new Guid("459ed664-167e-49d9-96bb-464d36fb5899"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, "Admin", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 2, 27, 14, 31, 2, 15, DateTimeKind.Local).AddTicks(2274) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientProducts_PatientId",
                table: "PatientProducts",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProducts_ProductId",
                table: "PatientProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PatientProductId",
                table: "Payments",
                column: "PatientProductId");
        }
    }
}
