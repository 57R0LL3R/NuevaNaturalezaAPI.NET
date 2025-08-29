using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class postgres5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Medicion__IdEsta__778AC167",
                table: "Medicion");

            migrationBuilder.DropIndex(
                name: "IX_Medicion_IdEstadoDispositivo",
                table: "Medicion");

            migrationBuilder.DropColumn(
                name: "IdEstadoDispositivo",
                table: "Medicion");


            migrationBuilder.AddColumn<Guid>(
                name: "IdEstadoDispositivo",
                table: "Dispositivo",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActuadorIdActuador",
                table: "Auditoria",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivo_IdEstadoDispositivo",
                table: "Dispositivo",
                column: "IdEstadoDispositivo");

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_ActuadorIdActuador",
                table: "Auditoria",
                column: "ActuadorIdActuador");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_Actuador_ActuadorIdActuador",
                table: "Auditoria",
                column: "ActuadorIdActuador",
                principalTable: "Actuador",
                principalColumn: "IdActuador");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivo_EstadoDispositivo_IdEstadoDispositivo",
                table: "Dispositivo",
                column: "IdEstadoDispositivo",
                principalTable: "EstadoDispositivo",
                principalColumn: "IdEstadoDispositivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_Actuador_ActuadorIdActuador",
                table: "Auditoria");

            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivo_EstadoDispositivo_IdEstadoDispositivo",
                table: "Dispositivo");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivo_IdEstadoDispositivo",
                table: "Dispositivo");

            migrationBuilder.DropIndex(
                name: "IX_Auditoria_ActuadorIdActuador",
                table: "Auditoria");


            migrationBuilder.DropColumn(
                name: "IdEstadoDispositivo",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "ActuadorIdActuador",
                table: "Auditoria");

            migrationBuilder.AddColumn<Guid>(
                name: "IdEstadoDispositivo",
                table: "Medicion",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicion_IdEstadoDispositivo",
                table: "Medicion",
                column: "IdEstadoDispositivo");

            migrationBuilder.AddForeignKey(
                name: "FK__Medicion__IdEsta__778AC167",
                table: "Medicion",
                column: "IdEstadoDispositivo",
                principalTable: "EstadoDispositivo",
                principalColumn: "IdEstadoDispositivo");
        }
    }
}
