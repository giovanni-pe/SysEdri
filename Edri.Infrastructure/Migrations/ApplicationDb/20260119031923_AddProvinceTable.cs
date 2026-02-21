using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddProvinceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Provinces_Departments_DepartmentId1",
                        column: x => x.DepartmentId1,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "Code", "DeletedAt", "DepartmentId", "DepartmentId1", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000001"), "01", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Huánuco" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000002"), "02", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Ambo" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000003"), "03", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Dos de Mayo" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000004"), "04", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Huacaybamba" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000005"), "05", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Huamalíes" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000006"), "06", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Leoncio Prado" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000007"), "07", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Marañón" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000008"), "08", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Pachitea" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000009"), "09", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Puerto Inca" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000010"), "10", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Lauricocha" },
                    { new Guid("a1b2c3d4-0000-0000-0000-100000000011"), "11", null, new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), null, "Yarowilca" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_DepartmentId_Code",
                table: "Provinces",
                columns: new[] { "DepartmentId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_DepartmentId1",
                table: "Provinces",
                column: "DepartmentId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
