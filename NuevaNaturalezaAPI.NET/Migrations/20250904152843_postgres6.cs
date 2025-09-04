using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class postgres6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuador_AccionAct_IdAccionAct",
                table: "Actuador");

            migrationBuilder.DropForeignKey(
                name: "FK__Medicion__IdUnid__76969D2E",
                table: "Medicion");

            migrationBuilder.DropForeignKey(
                name: "FK__PuntoOpti__IdUni__628FA481",
                table: "PuntoOptimo");

            migrationBuilder.DropForeignKey(
                name: "FK__Sensor__IdTipoMe__5DCAEF64",
                table: "Sensor");

            migrationBuilder.DropForeignKey(
                name: "FK__Sensor__IdUnidad__5EBF139D",
                table: "Sensor");

            migrationBuilder.DropIndex(
                name: "IX_Sensor_IdTipoMedicion",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "IdTipoMedicion",
                table: "Sensor");

            migrationBuilder.RenameColumn(
                name: "IdUnidadMedida",
                table: "Sensor",
                newName: "IdTipoMUnidadM");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_IdUnidadMedida",
                table: "Sensor",
                newName: "IX_Sensor_IdTipoMUnidadM");

            migrationBuilder.RenameColumn(
                name: "IdUnidadMedida",
                table: "PuntoOptimo",
                newName: "IdTipoMUnidadM");

            migrationBuilder.RenameIndex(
                name: "IX_PuntoOptimo_IdUnidadMedida",
                table: "PuntoOptimo",
                newName: "IX_PuntoOptimo_IdTipoMUnidadM");

            migrationBuilder.RenameColumn(
                name: "IdUnidadMedida",
                table: "Medicion",
                newName: "IdTipoMUnidadM");

            migrationBuilder.RenameIndex(
                name: "IX_Medicion_IdUnidadMedida",
                table: "Medicion",
                newName: "IX_Medicion_IdTipoMUnidadM");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdAccionAct",
                table: "Actuador",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuador_AccionAct_IdAccionAct",
                table: "Actuador",
                column: "IdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct");

            migrationBuilder.AddForeignKey(
                name: "FK__Medicion__IdUnid__76969D2E",
                table: "Medicion",
                column: "IdTipoMUnidadM",
                principalTable: "TipoM_UnidadM",
                principalColumn: "IdTipoM_UnidadM");

            migrationBuilder.AddForeignKey(
                name: "FK__PuntoOpti__IdUni__628FA481",
                table: "PuntoOptimo",
                column: "IdTipoMUnidadM",
                principalTable: "TipoM_UnidadM",
                principalColumn: "IdTipoM_UnidadM");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensor_TipoM_UnidadM_IdTipoMUnidadM",
                table: "Sensor",
                column: "IdTipoMUnidadM",
                principalTable: "TipoM_UnidadM",
                principalColumn: "IdTipoM_UnidadM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actuador_AccionAct_IdAccionAct",
                table: "Actuador");

            migrationBuilder.DropForeignKey(
                name: "FK__Medicion__IdUnid__76969D2E",
                table: "Medicion");

            migrationBuilder.DropForeignKey(
                name: "FK__PuntoOpti__IdUni__628FA481",
                table: "PuntoOptimo");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensor_TipoM_UnidadM_IdTipoMUnidadM",
                table: "Sensor");

            migrationBuilder.RenameColumn(
                name: "IdTipoMUnidadM",
                table: "Sensor",
                newName: "IdUnidadMedida");

            migrationBuilder.RenameIndex(
                name: "IX_Sensor_IdTipoMUnidadM",
                table: "Sensor",
                newName: "IX_Sensor_IdUnidadMedida");

            migrationBuilder.RenameColumn(
                name: "IdTipoMUnidadM",
                table: "PuntoOptimo",
                newName: "IdUnidadMedida");

            migrationBuilder.RenameIndex(
                name: "IX_PuntoOptimo_IdTipoMUnidadM",
                table: "PuntoOptimo",
                newName: "IX_PuntoOptimo_IdUnidadMedida");

            migrationBuilder.RenameColumn(
                name: "IdTipoMUnidadM",
                table: "Medicion",
                newName: "IdUnidadMedida");

            migrationBuilder.RenameIndex(
                name: "IX_Medicion_IdTipoMUnidadM",
                table: "Medicion",
                newName: "IX_Medicion_IdUnidadMedida");

            migrationBuilder.AddColumn<Guid>(
                name: "IdTipoMedicion",
                table: "Sensor",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdAccionAct",
                table: "Actuador",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_IdTipoMedicion",
                table: "Sensor",
                column: "IdTipoMedicion");

            migrationBuilder.AddForeignKey(
                name: "FK_Actuador_AccionAct_IdAccionAct",
                table: "Actuador",
                column: "IdAccionAct",
                principalTable: "AccionAct",
                principalColumn: "IdAccionAct",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Medicion__IdUnid__76969D2E",
                table: "Medicion",
                column: "IdUnidadMedida",
                principalTable: "UnidadMedida",
                principalColumn: "IdUnidadMedida");

            migrationBuilder.AddForeignKey(
                name: "FK__PuntoOpti__IdUni__628FA481",
                table: "PuntoOptimo",
                column: "IdUnidadMedida",
                principalTable: "UnidadMedida",
                principalColumn: "IdUnidadMedida");

            migrationBuilder.AddForeignKey(
                name: "FK__Sensor__IdTipoMe__5DCAEF64",
                table: "Sensor",
                column: "IdTipoMedicion",
                principalTable: "TipoMedicion",
                principalColumn: "IdTipoMedicion");

            migrationBuilder.AddForeignKey(
                name: "FK__Sensor__IdUnidad__5EBF139D",
                table: "Sensor",
                column: "IdUnidadMedida",
                principalTable: "UnidadMedida",
                principalColumn: "IdUnidadMedida");
        }
    }
}
