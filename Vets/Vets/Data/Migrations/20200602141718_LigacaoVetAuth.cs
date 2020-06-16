using Microsoft.EntityFrameworkCore.Migrations;

namespace Vets.Data.Migrations
{
    public partial class LigacaoVetAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Veterinarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Veterinarios");
        }
    }
}
