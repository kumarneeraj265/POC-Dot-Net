using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_PRAC.Migrations
{
    public partial class NewDBStructureForPlayerNew3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Availablities",
                table: "Availablities");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "TempPlayers");

            migrationBuilder.RenameTable(
                name: "Availablities",
                newName: "TempAvailablities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempPlayers",
                table: "TempPlayers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempAvailablities",
                table: "TempAvailablities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TempPlayers",
                table: "TempPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempAvailablities",
                table: "TempAvailablities");

            migrationBuilder.RenameTable(
                name: "TempPlayers",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "TempAvailablities",
                newName: "Availablities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availablities",
                table: "Availablities",
                column: "Id");
        }
    }
}
