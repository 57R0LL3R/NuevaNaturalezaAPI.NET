using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnusuarioinchecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Checklist");

            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuario",
                table: "Checklist",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_IdUsuario",
                table: "Checklist",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Checklist_Usuario_IdUsuario",
                table: "Checklist",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checklist_Usuario_IdUsuario",
                table: "Checklist");

            migrationBuilder.DropIndex(
                name: "IX_Checklist_IdUsuario",
                table: "Checklist");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Checklist");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Checklist",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
