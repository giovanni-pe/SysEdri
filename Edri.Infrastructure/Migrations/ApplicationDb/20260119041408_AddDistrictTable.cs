using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddDistrictTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character(6)", fixedLength: true, maxLength: 6, nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProvinceId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Districts_Provinces_ProvinceId1",
                        column: x => x.ProvinceId1,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "Id", "Code", "DeletedAt", "Name", "ProvinceId", "ProvinceId1" },
                values: new object[,]
                {
                    { new Guid("d7f8f0a1-0000-0000-0000-100601000001"), "100601", null, "Rupa-Rupa", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100602000002"), "100602", null, "Daniel Alomía Robles", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100603000003"), "100603", null, "Hermilio Valdizán", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100604000004"), "100604", null, "José Crespo y Castillo", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100605000005"), "100605", null, "Luyando", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100606000006"), "100606", null, "Mariano Dámaso Beraún", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100607000007"), "100607", null, "Pucayacu", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100608000008"), "100608", null, "Castillo Grande", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100609000009"), "100609", null, "Pueblo Nuevo", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null },
                    { new Guid("d7f8f0a1-0000-0000-0000-100610000010"), "100610", null, "Santo Domingo de Anda", new Guid("a1b2c3d4-0000-0000-0000-100000000006"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Districts_Code",
                table: "Districts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId",
                table: "Districts",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ProvinceId1",
                table: "Districts",
                column: "ProvinceId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Districts");
        }
    }
}
