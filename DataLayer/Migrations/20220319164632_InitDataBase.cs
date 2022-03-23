using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonEntities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CodeMelli = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Education = table.Column<int>(type: "int", nullable: false),
                    Reshteh = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Gerayesh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Moaadel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UniversityName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UnivesityType = table.Column<int>(type: "int", nullable: false),
                    UniversityAddress = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    EnterToUniversityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExiteFromUniversityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEntities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonEntities_CodeMelli",
                table: "PersonEntities",
                column: "CodeMelli",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonEntities");
        }
    }
}
