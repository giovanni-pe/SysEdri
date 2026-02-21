using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddSupplyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplyNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TariffId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstallationAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Reference = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Latitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ActivationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TerminationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplies_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplies_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplies_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Supplies",
                columns: new[] { "Id", "ActivationDate", "BranchId", "CustomerId", "DeletedAt", "DistrictId", "InstallationAddress", "Latitude", "Longitude", "Reference", "Status", "SupplyNumber", "TariffId", "TerminationDate" },
                values: new object[,]
                {
                    { new Guid("d1000000-0000-0000-0000-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("f1000000-0000-0000-0000-000000000001"), new Guid("b1000000-0000-0000-0000-000000000001"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "Av. Amazonas 123, Tingo María", -9.2950m, -76.0000m, "Frente al parque principal", "Active", "SUM-2025-000001", new Guid("aa000000-0000-0000-0000-000000000001"), null },
                    { new Guid("d1000000-0000-0000-0000-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("f1000000-0000-0000-0000-000000000001"), new Guid("b1000000-0000-0000-0000-000000000002"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "Jr. Ucayali 456, Tingo María", -9.2960m, -76.0010m, "Cerca del mercado", "Active", "SUM-2025-000002", new Guid("aa000000-0000-0000-0000-000000000001"), null },
                    { new Guid("d1000000-0000-0000-0000-000000000003"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("f1000000-0000-0000-0000-000000000001"), new Guid("b1000000-0000-0000-0000-000000000003"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "Av. Raymondi 789, Tingo María", -9.2940m, -75.9990m, "Local comercial esquina", "Active", "SUM-2025-000003", new Guid("aa000000-0000-0000-0000-000000000003"), null },
                    { new Guid("d1000000-0000-0000-0000-000000000004"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("f1000000-0000-0000-0000-000000000001"), new Guid("b1000000-0000-0000-0000-000000000004"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "Carretera Central Km 5, Tingo María", -9.2800m, -75.9850m, "Zona industrial", "Active", "SUM-2025-000004", new Guid("aa000000-0000-0000-0000-000000000005"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_BranchId",
                table: "Supplies",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_CustomerId",
                table: "Supplies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_DistrictId",
                table: "Supplies",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_Status",
                table: "Supplies",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SupplyNumber",
                table: "Supplies",
                column: "SupplyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_TariffId",
                table: "Supplies",
                column: "TariffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Supplies");
        }
    }
}
