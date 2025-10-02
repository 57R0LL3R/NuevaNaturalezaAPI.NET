using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class AddChecklistTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SegundoNombre",
                table: "Dispositivo",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    IdChecklist = table.Column<Guid>(type: "uuid", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Usuario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ObservacionesGenerales = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.IdChecklist);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistDetalle",
                columns: table => new
                {
                    IdDetalle = table.Column<Guid>(type: "uuid", nullable: false),
                    IdChecklist = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDispositivo = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValorRegistrado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistDetalle", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_ChecklistDetalle_Checklist_IdChecklist",
                        column: x => x.IdChecklist,
                        principalTable: "Checklist",
                        principalColumn: "IdChecklist",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistDetalle_Dispositivo_IdDispositivo",
                        column: x => x.IdDispositivo,
                        principalTable: "Dispositivo",
                        principalColumn: "IdDispositivo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetalle_IdChecklist",
                table: "ChecklistDetalle",
                column: "IdChecklist");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistDetalle_IdDispositivo",
                table: "ChecklistDetalle",
                column: "IdDispositivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistDetalle");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.AlterColumn<string>(
                name: "SegundoNombre",
                table: "Dispositivo",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
