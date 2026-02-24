using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddDeviceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MeterId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    MacAddress = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: true),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FirmwareVersion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastConnection = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "BranchId", "Code", "DeletedAt", "FirmwareVersion", "InstallationDate", "LastConnection", "MacAddress", "MeterId", "Model", "Status" },
                values: new object[,]
                {
                    { new Guid("de000000-0000-0000-0000-000000000001"), new Guid("f1000000-0000-0000-0000-000000000001"), "IOT-TM-001", null, "1.0.0", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 20, 10, 30, 0, 0, DateTimeKind.Utc), "AA:BB:CC:DD:EE:01", new Guid("c2000000-0000-0000-0000-000000000001"), "ESP32-CAM", 1 },
                    { new Guid("de000000-0000-0000-0000-000000000002"), new Guid("f1000000-0000-0000-0000-000000000001"), "IOT-TM-002", null, "1.0.0", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 20, 10, 30, 0, 0, DateTimeKind.Utc), "AA:BB:CC:DD:EE:02", new Guid("c2000000-0000-0000-0000-000000000002"), "ESP32-CAM", 1 },
                    { new Guid("de000000-0000-0000-0000-000000000003"), new Guid("f1000000-0000-0000-0000-000000000001"), "IOT-TM-003", null, "2.1.0", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 20, 10, 30, 0, 0, DateTimeKind.Utc), "AA:BB:CC:DD:EE:03", new Guid("c2000000-0000-0000-0000-000000000003"), "Raspberry Pi 4", 1 },
                    { new Guid("de000000-0000-0000-0000-000000000004"), new Guid("f1000000-0000-0000-0000-000000000001"), "IOT-TM-004", null, "2.1.0", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, "AA:BB:CC:DD:EE:04", new Guid("c2000000-0000-0000-0000-000000000004"), "Raspberry Pi 4", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BranchId",
                table: "Devices",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Code",
                table: "Devices",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MeterId",
                table: "Devices",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Status",
                table: "Devices",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
