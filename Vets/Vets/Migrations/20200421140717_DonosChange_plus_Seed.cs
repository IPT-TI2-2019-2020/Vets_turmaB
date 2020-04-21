using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vets.Migrations
{
    public partial class DonosChange_plus_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Donos",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Donos",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Donos",
                maxLength: 1,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Donos",
                columns: new[] { "ID", "NIF", "Nome", "Sexo" },
                values: new object[,]
                {
                    { 1, "813635582", "Luís Freitas", "M" },
                    { 2, "854613462", "Andreia Gomes", "F" },
                    { 3, "265368715", "Cristina Sousa", "F" },
                    { 4, "835623190", "Sónia Rosa", "F" },
                    { 5, "751512205", "António Santos", "M" },
                    { 6, "728663835", "Gustavo Alves", "M" },
                    { 7, "644388118", "Rosa Vieira", "F" },
                    { 8, "262618487", "Daniel Dias", "M" },
                    { 9, "842615197", "Tânia Gomes", "F" },
                    { 10, "635139506", "Andreia Correia", "F" }
                });

            migrationBuilder.InsertData(
                table: "Veterinarios",
                columns: new[] { "ID", "Fotografia", "Nome", "NumCedulaProf" },
                values: new object[,]
                {
                    { 1, "Maria.jpg", "Maria Pinto", " vet-34589" },
                    { 2, "Ricardo.jpg", "Ricardo Ribeiro", " vet-34590" },
                    { 3, "Jose.jpg", "José Soares", " vet-56732" }
                });

            migrationBuilder.InsertData(
                table: "Animais",
                columns: new[] { "ID", "DonoFK", "Especie", "Foto", "Nome", "Peso", "Raca" },
                values: new object[,]
                {
                    { 1, 1, "Cão", "animal1.jpg", "Bubi", 24.0, "Pastor Alemão" },
                    { 10, 1, "Gato", "animal10.jpg", "Saltitão", 2.0, "Persa" },
                    { 3, 2, "Cão", "animal3.jpg", "Tripé", 4.0, "Serra Estrela" },
                    { 2, 3, "Cão", "animal2.jpg", "Pastor", 50.0, "Serra Estrela" },
                    { 4, 3, "Cavalo", "animal4.jpg", "Saltador", 580.0, "Lusitana" },
                    { 5, 3, "Gato", "animal5.jpg", "Tareco", 1.0, "siamês" },
                    { 8, 7, "Cão", "animal8.jpg", "Forte", 20.0, "Rottweiler" },
                    { 9, 8, "Vaca", "animal9.jpg", "Castanho", 652.0, "Mirandesa" },
                    { 6, 9, "Cão", "animal6.jpg", "Cusca", 45.0, "Labrador" },
                    { 7, 10, "Cão", "animal7.jpg", "Morde Tudo", 39.0, "Dobermann" }
                });

            migrationBuilder.InsertData(
                table: "Consultas",
                columns: new[] { "ID", "AnimalFK", "Data", "Observacoes", "VeterinarioFK" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 16, 6, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 7, 6, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 17, 9, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 10, 9, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 5, 9, new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 9, 8, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 20, 5, new DateTime(2020, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 15, 5, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 6, 5, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 4, 4, new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 14, 2, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 3, 2, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, 3, new DateTime(2020, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 19, 10, new DateTime(2020, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 11, 10, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 18, 1, new DateTime(2020, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 13, 1, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 8, 7, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3 },
                    { 12, 7, new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Consultas",
                keyColumn: "ID",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animais",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Veterinarios",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Veterinarios",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Veterinarios",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Donos",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Donos");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Donos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Donos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }
    }
}
