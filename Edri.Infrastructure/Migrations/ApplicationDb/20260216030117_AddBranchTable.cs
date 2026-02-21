using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddBranchTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistrictId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Branches_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Address", "CompanyId", "DeletedAt", "DistrictId", "IsActive", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("f1000000-0000-0000-0000-000000000001"), "Av. Amazonas 456, Tingo María", new Guid("e1000000-0000-0000-0000-000000000001"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), true, "Sucursal Tingo María", "062561234" },
                    { new Guid("f1000000-0000-0000-0000-000000000002"), "Jr. Raymondi 123, Tingo María", new Guid("e1000000-0000-0000-0000-000000000001"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), true, "Sucursal Centro", "062565678" },
                    { new Guid("f1000000-0000-0000-0000-000000000003"), "Av. Universitaria 789, Tingo María", new Guid("e1000000-0000-0000-0000-000000000002"), null, new Guid("d7f8f0a1-0000-0000-0000-100601000001"), true, "Agencia Leoncio Prado", "062569012" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyId_Name",
                table: "Branches",
                columns: new[] { "CompanyId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_DistrictId",
                table: "Branches",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
