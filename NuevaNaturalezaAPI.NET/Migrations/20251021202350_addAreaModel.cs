using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class addAreaModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdArea",
                table: "Dispositivo",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    IdArea = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.IdArea);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivo_Area_IdDispositivo",
                table: "Dispositivo",
                column: "IdDispositivo",
                principalTable: "Area",
                principalColumn: "IdArea",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivo_Area_IdDispositivo",
                table: "Dispositivo");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropColumn(
                name: "IdArea",
                table: "Dispositivo");
        }
    }
}
