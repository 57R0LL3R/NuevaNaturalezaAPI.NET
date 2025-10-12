using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramacionDosificadorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramacionDosificador",
                columns: table => new
                {
                    IdProgramacion = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDosificador = table.Column<Guid>(type: "uuid", nullable: false),
                    Hora = table.Column<int>(type: "integer", nullable: false),
                    Minuto = table.Column<int>(type: "integer", nullable: false),
                    TiempoSegundos = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramacionDosificador", x => x.IdProgramacion);
                    table.ForeignKey(
                        name: "FK_ProgramacionDosificador_Dispositivo",
                        column: x => x.IdDosificador,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionDosificador_IdDosificador",
                table: "ProgramacionDosificador",
                column: "IdDosificador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramacionDosificador");
        }
    }
}
