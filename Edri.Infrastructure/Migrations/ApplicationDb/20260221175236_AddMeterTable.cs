using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddMeterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MeterNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SupplyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AmperageCapacity = table.Column<int>(type: "integer", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastCalibrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meters_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Meters",
                columns: new[] { "Id", "AmperageCapacity", "Brand", "DeletedAt", "InstallationDate", "LastCalibrationDate", "MeterNumber", "Model", "Status", "SupplyId", "Type" },
                values: new object[,]
                {
                    { new Guid("c2000000-0000-0000-0000-000000000001"), 30, "ELSTER", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "MED-2025-000001", "A1100", 1, new Guid("d1000000-0000-0000-0000-000000000001"), 1 },
                    { new Guid("c2000000-0000-0000-0000-000000000002"), 40, "LANDIS+GYR", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "MED-2025-000002", "E350", 1, new Guid("d1000000-0000-0000-0000-000000000002"), 2 },
                    { new Guid("c2000000-0000-0000-0000-000000000003"), 60, "ITRON", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "MED-2025-000003", "OpenWay Riva", 1, new Guid("d1000000-0000-0000-0000-000000000003"), 3 },
                    { new Guid("c2000000-0000-0000-0000-000000000004"), 100, "SCHNEIDER", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "MED-2025-000004", "ION8650", 1, new Guid("d1000000-0000-0000-0000-000000000004"), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meters_MeterNumber",
                table: "Meters",
                column: "MeterNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meters_Status",
                table: "Meters",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_SupplyId",
                table: "Meters",
                column: "SupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meters");
        }
    }
}
