using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class Add_ExcesoPuntoOptimo : Migration
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
                name: "TipoExceso",
                columns: table => new
                {
                    IdTipoExceso = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExceso", x => x.IdTipoExceso);
                });

            migrationBuilder.CreateTable(
                name: "ExcesoPuntoOptimo",
                columns: table => new
                {
                    IdExcesoPuntoOptimo = table.Column<Guid>(type: "uuid", nullable: false),
                    IdActuador = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAccionAct = table.Column<Guid>(type: "uuid", nullable: true),
                    IdPuntoOptimo = table.Column<Guid>(type: "uuid", nullable: true),
                    IdTipoExceso = table.Column<Guid>(type: "uuid", nullable: true),
                    IdActuadorNavigationIdActuador = table.Column<Guid>(type: "uuid", nullable: true),
                    IdAccionActNavigationIdAccionAct = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcesoPuntoOptimo", x => x.IdExcesoPuntoOptimo);
                    table.ForeignKey(
                        name: "FK_ExcesoPuntoOptimo_AccionAct_IdAccionActNavigationIdAccionAct",
                        column: x => x.IdAccionActNavigationIdAccionAct,
                        principalTable: "AccionAct",
                        principalColumn: "IdAccionAct");
                    table.ForeignKey(
                        name: "FK_ExcesoPuntoOptimo_Actuador_IdActuadorNavigationIdActuador",
                        column: x => x.IdActuadorNavigationIdActuador,
                        principalTable: "Actuador",
                        principalColumn: "IdActuador");
                    table.ForeignKey(
                        name: "FK_ExcesoPuntoOptimo_PuntoOptimo_IdPuntoOptimo",
                        column: x => x.IdPuntoOptimo,
                        principalTable: "PuntoOptimo",
                        principalColumn: "IdPuntoOptimo");
                    table.ForeignKey(
                        name: "FK_ExcesoPuntoOptimo_TipoExceso_IdTipoExceso",
                        column: x => x.IdTipoExceso,
                        principalTable: "TipoExceso",
                        principalColumn: "IdTipoExceso");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo",
                column: "IdAccionActNavigationIdAccionAct");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuadorNavigationIdActuador");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdPuntoOptimo",
                table: "ExcesoPuntoOptimo",
                column: "IdPuntoOptimo");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdTipoExceso",
                table: "ExcesoPuntoOptimo",
                column: "IdTipoExceso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcesoPuntoOptimo");

            migrationBuilder.DropTable(
                name: "TipoExceso");

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
