using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Edri.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddDepartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "DeletedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000001"), "01", null, "Amazonas" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000002"), "02", null, "Áncash" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000003"), "03", null, "Apurímac" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000004"), "04", null, "Arequipa" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000005"), "05", null, "Ayacucho" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000006"), "06", null, "Cajamarca" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000007"), "07", null, "Callao" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000008"), "08", null, "Cusco" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000009"), "09", null, "Huancavelica" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000010"), "10", null, "Huánuco" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000011"), "11", null, "Ica" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000012"), "12", null, "Junín" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000013"), "13", null, "La Libertad" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000014"), "14", null, "Lambayeque" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000015"), "15", null, "Lima" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000016"), "16", null, "Loreto" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000017"), "17", null, "Madre de Dios" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000018"), "18", null, "Moquegua" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000019"), "19", null, "Pasco" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000020"), "20", null, "Piura" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000021"), "21", null, "Puno" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000022"), "22", null, "San Martín" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000023"), "23", null, "Tacna" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000024"), "24", null, "Tumbes" },
                    { new Guid("d7f8f0a1-1b1a-4b1a-8b1a-000000000025"), "25", null, "Ucayali" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                table: "Departments",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
