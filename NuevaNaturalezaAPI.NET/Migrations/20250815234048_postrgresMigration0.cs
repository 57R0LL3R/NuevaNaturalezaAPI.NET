using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class postrgresMigration0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccionAct",
                columns: table => new
                {
                    IdAccionAct = table.Column<Guid>(type: "uuid", nullable: false),
                    Accion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccionAct", x => x.IdAccionAct);
                });

            migrationBuilder.CreateTable(
                name: "Actuador",
                columns: table => new
                {
                    IdActuador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAccionAct = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuador", x => x.IdActuador);
                    table.ForeignKey(
                        name: "FK_Actuador_AccionAct_IdAccionAct",
                        column: x => x.IdAccionAct,
                        principalTable: "AccionAct",
                        principalColumn: "IdAccionAct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actuador_Dispositivo_IdDispositivo",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actuador_IdAccionAct",
                table: "Actuador",
                column: "IdAccionAct");

            migrationBuilder.CreateIndex(
                name: "IX_Actuador_IdDispositivo",
                table: "Actuador",
                column: "IdDispositivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actuador");

            migrationBuilder.DropTable(
                name: "AccionAct");
        }
    }
}
