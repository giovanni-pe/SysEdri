using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddCustomerSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CustomerType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BusinessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "BusinessName", "CustomerType", "DeletedAt", "DistrictId", "DocumentNumber", "DocumentType", "Email", "FirstName", "LastName", "Phone", "RegistrationDate", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("b1000000-0000-0000-0000-000000000001"), "Av. Amazonas 123, Tingo María", null, "Residential", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "12345678", "DNI", "juan.perez@email.com", "Juan", "Pérez García", "962123456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Active", null, new Guid("a1000000-0000-0000-0000-000000000001") },
                    { new Guid("b1000000-0000-0000-0000-000000000002"), "Jr. Ucayali 456, Tingo María", null, "Residential", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "87654321", "DNI", "maria.lopez@email.com", "María", "López Rodríguez", "962654321", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Active", null, new Guid("a1000000-0000-0000-0000-000000000002") },
                    { new Guid("b1000000-0000-0000-0000-000000000003"), "Av. Raymondi 789, Tingo María", "Comercial Los Andes S.A.C.", "Commercial", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "20123456789", "RUC", "contacto@losandes.com", null, null, "062562000", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Active", null, new Guid("a1000000-0000-0000-0000-000000000003") },
                    { new Guid("b1000000-0000-0000-0000-000000000004"), "Carretera Central Km 5, Tingo María", "Agroindustrias del Oriente S.A.", "Industrial", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "20987654321", "RUC", "info@agroindustriasoriente.com", null, null, "062563000", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Active", null, new Guid("a1000000-0000-0000-0000-000000000004") },
                    { new Guid("b1000000-0000-0000-0000-000000000005"), "Jr. Huánuco 321, Tingo María", null, "Residential", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "45678912", "DNI", "carlos.mendoza@email.com", "Carlos", "Mendoza Silva", "962789456", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Suspended", null, new Guid("a1000000-0000-0000-0000-000000000005") },
                    { new Guid("b1000000-0000-0000-0000-000000000006"), "Av. Universitaria 555, Tingo María", null, "Residential", null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "CE123456", "CE", "roberto.gonzalez@email.com", "Roberto", "González Martínez", "962111222", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Active", null, new Guid("a1000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DistrictId",
                table: "Customers",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DocumentNumber",
                table: "Customers",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Status",
                table: "Customers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
