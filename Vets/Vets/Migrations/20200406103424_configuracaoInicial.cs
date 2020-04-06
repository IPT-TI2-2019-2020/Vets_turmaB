using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vets.Migrations
{
    public partial class configuracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    NIF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    NumCedulaProf = table.Column<string>(nullable: true),
                    Fotografia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Especie = table.Column<string>(nullable: true),
                    Raca = table.Column<string>(nullable: true),
                    Peso = table.Column<double>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    DonoFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animais_Donos_DonoFK",
                        column: x => x.DonoFK,
                        principalTable: "Donos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true),
                    AnimalFK = table.Column<int>(nullable: false),
                    VeterinarioFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consultas_Animais_AnimalFK",
                        column: x => x.AnimalFK,
                        principalTable: "Animais",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Veterinarios_VeterinarioFK",
                        column: x => x.VeterinarioFK,
                        principalTable: "Veterinarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animais_DonoFK",
                table: "Animais",
                column: "DonoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_AnimalFK",
                table: "Consultas",
                column: "AnimalFK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_VeterinarioFK",
                table: "Consultas",
                column: "VeterinarioFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.DropTable(
                name: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Donos");
        }
    }
}
