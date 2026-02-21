using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddTariffTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PricePerKwh = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    FixedCharge = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tariffs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "Code", "CompanyId", "DeletedAt", "Description", "EffectiveFrom", "EffectiveTo", "FixedCharge", "IsActive", "Name", "PricePerKwh" },
                values: new object[,]
                {
                    { new Guid("aa000000-0000-0000-0000-000000000001"), "BT5B", new Guid("e1000000-0000-0000-0000-000000000001"), null, "Tarifa para consumo residencial hasta 100 kWh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3.26m, true, "Tarifa Residencial BT5B", 0.7520m },
                    { new Guid("aa000000-0000-0000-0000-000000000002"), "BT5A", new Guid("e1000000-0000-0000-0000-000000000001"), null, "Tarifa para consumo residencial mayor a 100 kWh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3.26m, true, "Tarifa Residencial BT5A", 0.8150m },
                    { new Guid("aa000000-0000-0000-0000-000000000003"), "BT4", new Guid("e1000000-0000-0000-0000-000000000001"), null, "Tarifa para uso comercial", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 5.50m, true, "Tarifa Comercial BT4", 0.6890m },
                    { new Guid("aa000000-0000-0000-0000-000000000004"), "BT5B", new Guid("e1000000-0000-0000-0000-000000000002"), null, "Tarifa para consumo residencial hasta 100 kWh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 3.10m, true, "Tarifa Residencial BT5B", 0.7350m },
                    { new Guid("aa000000-0000-0000-0000-000000000005"), "MT3", new Guid("e1000000-0000-0000-0000-000000000002"), null, "Tarifa para uso industrial en media tensión", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, 25.00m, true, "Tarifa Industrial MT3", 0.5200m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tariffs_CompanyId_Code",
                table: "Tariffs",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tariffs");
        }
    }
}
