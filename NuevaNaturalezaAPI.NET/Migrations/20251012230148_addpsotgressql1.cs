using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class addpsotgressql1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dosificadores",
                columns: table => new
                {
                    IdDosificador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    LetraActivacion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dosificadores", x => x.IdDosificador);
                    table.ForeignKey(
                        name: "FK_Dosificadores_Dispositivo_IdDispositivo",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "ProgramacionDosificadores",
                columns: table => new
                {
                    IdProgramacion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDosificador = table.Column<Guid>(type: "uuid", nullable: false),
                    Hora = table.Column<int>(type: "integer", nullable: false),
                    Minuto = table.Column<int>(type: "integer", nullable: false),
                    TiempoSegundos = table.Column<int>(type: "integer", nullable: false),
                    DispositivoIdDispositivo = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionDosificadores", x => x.IdProgramacion);
                    table.ForeignKey(
                        name: "FK_ProgramacionDosificadores_Dispositivo_DispositivoIdDisposit~",
                        column: x => x.DispositivoIdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo");
                    table.ForeignKey(
                        name: "FK_ProgramacionDosificadores_Dosificadores_IdDosificador",
                        column: x => x.IdDosificador,
                        principalTable: "Dosificadores",
                        principalColumn: "IdDosificador",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Dosificadores_IdDispositivo",
                table: "Dosificadores",
                column: "IdDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionDosificadores_DispositivoIdDispositivo",
                table: "ProgramacionDosificadores",
                column: "DispositivoIdDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionDosificadores_IdDosificador",
                table: "ProgramacionDosificadores",
                column: "IdDosificador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "ProgramacionDosificadores");



            migrationBuilder.DropTable(
                name: "Dosificadores");
        }
    }
}
