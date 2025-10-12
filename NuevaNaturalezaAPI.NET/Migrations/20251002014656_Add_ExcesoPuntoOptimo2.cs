using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class Add_ExcesoPuntoOptimo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.RenameColumn(
                name: "IdActuador",
                table: "ExcesoPuntoOptimo",
                newName: "IdDispositivo");

            migrationBuilder.RenameIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuador",
                table: "ExcesoPuntoOptimo",
                newName: "IX_ExcesoPuntoOptimo_IdDispositivo");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_Dispositivo_IdDispositivo",
                table: "ExcesoPuntoOptimo",
                column: "IdDispositivo",
                principalTable: "Dispositivo",
                principalColumn: "IdDispositivo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_Dispositivo_IdDispositivo",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.RenameColumn(
                name: "IdDispositivo",
                table: "ExcesoPuntoOptimo",
                newName: "IdActuador");

            migrationBuilder.RenameIndex(
                name: "IX_ExcesoPuntoOptimo_IdDispositivo",
                table: "ExcesoPuntoOptimo",
                newName: "IX_ExcesoPuntoOptimo_IdActuador");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuador",
                principalTable: "Actuador",
                principalColumn: "IdActuador",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
