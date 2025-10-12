using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class Add_ExcesoPuntoOptimo1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_AccionAct_IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropIndex(
                name: "IX_ExcesoPuntoOptimo_IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropColumn(
                name: "IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropColumn(
                name: "IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdAccionAct",
                table: "ExcesoPuntoOptimo",
                column: "IdAccionAct");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuador");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_AccionAct_IdAccionAct",
                table: "ExcesoPuntoOptimo",
                column: "IdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuador",
                principalTable: "Actuador",
                principalColumn: "IdActuador",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_AccionAct_IdAccionAct",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropIndex(
                name: "IX_ExcesoPuntoOptimo_IdAccionAct",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.DropIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuador",
                table: "ExcesoPuntoOptimo");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo",
                column: "IdAccionActNavigationIdAccionAct");

            migrationBuilder.CreateIndex(
                name: "IX_ExcesoPuntoOptimo_IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuadorNavigationIdActuador");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_AccionAct_IdAccionActNavigationIdAccionAct",
                table: "ExcesoPuntoOptimo",
                column: "IdAccionActNavigationIdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcesoPuntoOptimo_Actuador_IdActuadorNavigationIdActuador",
                table: "ExcesoPuntoOptimo",
                column: "IdActuadorNavigationIdActuador",
                principalTable: "Actuador",
                principalColumn: "IdActuador");
        }
    }
}
