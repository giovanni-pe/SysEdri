using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddCaptureTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Captures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Captures_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Captures",
                columns: new[] { "Id", "DeletedAt", "DeviceId", "ImageUrl", "Status", "Timestamp" },
                values: new object[,]
                {
                    { new Guid("ca000000-0000-0000-0000-000000000001"), null, new Guid("de000000-0000-0000-0000-000000000001"), "/captures/2025/01/15/IOT-TM-001_080000.jpg", 2, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ca000000-0000-0000-0000-000000000002"), null, new Guid("de000000-0000-0000-0000-000000000001"), "/captures/2025/01/20/IOT-TM-001_080000.jpg", 2, new DateTime(2025, 1, 20, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ca000000-0000-0000-0000-000000000003"), null, new Guid("de000000-0000-0000-0000-000000000002"), "/captures/2025/01/15/IOT-TM-002_080500.jpg", 2, new DateTime(2025, 1, 15, 8, 5, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ca000000-0000-0000-0000-000000000004"), null, new Guid("de000000-0000-0000-0000-000000000002"), "/captures/2025/01/20/IOT-TM-002_080500.jpg", 1, new DateTime(2025, 1, 20, 8, 5, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ca000000-0000-0000-0000-000000000005"), null, new Guid("de000000-0000-0000-0000-000000000003"), "/captures/2025/01/15/IOT-TM-003_081000.jpg", 2, new DateTime(2025, 1, 15, 8, 10, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ca000000-0000-0000-0000-000000000006"), null, new Guid("de000000-0000-0000-0000-000000000003"), "/captures/2025/01/20/IOT-TM-003_080000.jpg", 3, new DateTime(2025, 1, 20, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Captures_DeviceId",
                table: "Captures",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Captures_Status",
                table: "Captures",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Captures_Timestamp",
                table: "Captures",
                column: "Timestamp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Captures");
        }
    }
}
