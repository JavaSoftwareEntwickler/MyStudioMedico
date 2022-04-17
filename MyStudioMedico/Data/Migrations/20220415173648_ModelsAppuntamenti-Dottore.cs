using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyStudioMedico.Data.Migrations
{
    public partial class ModelsAppuntamentiDottore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dottori",
                columns: table => new
                {
                    DottoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dottori", x => x.DottoreID);
                });

            migrationBuilder.CreateTable(
                name: "Appuntamenti",
                columns: table => new
                {
                    AppuntamentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DottoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appuntamenti", x => x.AppuntamentoID);
                    table.ForeignKey(
                        name: "FK_Appuntamenti_Dottori_DottoreID",
                        column: x => x.DottoreID,
                        principalTable: "Dottori",
                        principalColumn: "DottoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appuntamenti_DottoreID",
                table: "Appuntamenti",
                column: "DottoreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appuntamenti");

            migrationBuilder.DropTable(
                name: "Dottori");
        }
    }
}
