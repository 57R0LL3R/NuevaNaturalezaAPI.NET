using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class addAccionToEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEvento",
                table: "Evento",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAccionAct",
                table: "Evento",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Auditoria",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_IdAccionAct",
                table: "Evento",
                column: "IdAccionAct");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_AccionAct_IdAccionAct",
                table: "Evento",
                column: "IdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_AccionAct_IdAccionAct",
                table: "Evento");

            migrationBuilder.DropIndex(
                name: "IX_Evento_IdAccionAct",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "IdAccionAct",
                table: "Evento");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaEvento",
                table: "Evento",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "Auditoria",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
