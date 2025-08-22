using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class update_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_AccionAct_IdAccion",
                table: "Auditoria");


            migrationBuilder.DropColumn(
                name: "IdAccion",
                table: "Auditoria");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha",
                table: "FechaMedicion",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Auditoria",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "IdAccionAct",
                table: "Auditoria",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdAccionAct",
                table: "Auditoria",
                column: "IdAccionAct");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_AccionAct_IdAccionAct",
                table: "Auditoria",
                column: "IdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditoria_AccionAct_IdAccionAct",
                table: "Auditoria");


            migrationBuilder.DropColumn(
                name: "IdAccionAct",
                table: "Auditoria");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Fecha",
                table: "FechaMedicion",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Auditoria",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdAccion",
                table: "Auditoria",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_IdAccion",
                table: "Auditoria",
                column: "IdAccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditoria_AccionAct_IdAccion",
                table: "Auditoria",
                column: "IdAccion",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");
        }
    }
}
