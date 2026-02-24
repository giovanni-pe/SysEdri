using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddReadingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CaptureId = table.Column<Guid>(type: "uuid", nullable: true),
                    MeterNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValueKwh = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ReadingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Source = table.Column<int>(type: "integer", nullable: false),
                    OcrConfidence = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    Observations = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readings_Captures_CaptureId",
                        column: x => x.CaptureId,
                        principalTable: "Captures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Readings_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Readings",
                columns: new[] { "Id", "CaptureId", "DeletedAt", "DeviceId", "MeterNumber", "Observations", "OcrConfidence", "ReadingDate", "Source", "ValueKwh" },
                values: new object[,]
                {
                    { new Guid("ae000000-0000-0000-0000-000000000001"), new Guid("ca000000-0000-0000-0000-000000000001"), null, new Guid("de000000-0000-0000-0000-000000000001"), "MED-2025-000001", null, 98.5m, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), 1, 1250.50m },
                    { new Guid("ae000000-0000-0000-0000-000000000002"), new Guid("ca000000-0000-0000-0000-000000000002"), null, new Guid("de000000-0000-0000-0000-000000000001"), "MED-2025-000001", null, 99.1m, new DateTime(2025, 1, 20, 8, 0, 0, 0, DateTimeKind.Utc), 1, 1320.75m },
                    { new Guid("ae000000-0000-0000-0000-000000000003"), new Guid("ca000000-0000-0000-0000-000000000003"), null, new Guid("de000000-0000-0000-0000-000000000002"), "MED-2025-000002", null, 97.8m, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), 1, 980.25m },
                    { new Guid("ae000000-0000-0000-0000-000000000004"), new Guid("ca000000-0000-0000-0000-000000000005"), null, new Guid("de000000-0000-0000-0000-000000000003"), "MED-2025-000003", null, 99.5m, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), 1, 5420.00m },
                    { new Guid("ae000000-0000-0000-0000-000000000005"), null, null, new Guid("de000000-0000-0000-0000-000000000004"), "MED-2025-000004", "Lectura manual - dispositivo en mantenimiento", null, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc), 2, 15800.00m },
                    { new Guid("ae000000-0000-0000-0000-000000000006"), null, null, new Guid("de000000-0000-0000-0000-000000000002"), "MED-2025-000002", "Lectura estimada basada en consumo promedio histórico", null, new DateTime(2025, 1, 20, 8, 0, 0, 0, DateTimeKind.Utc), 3, 1045.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_CaptureId",
                table: "Readings",
                column: "CaptureId");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_DeviceId",
                table: "Readings",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_MeterNumber",
                table: "Readings",
                column: "MeterNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Readings_MeterNumber_ReadingDate",
                table: "Readings",
                columns: new[] { "MeterNumber", "ReadingDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_ReadingDate",
                table: "Readings",
                column: "ReadingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readings");
        }
    }
}
