using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class addpsotgressql2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "IdDosificador",
                table: "Dosificadores",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValueSql: "NEWID()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "IdDosificador",
                table: "Dosificadores",
                type: "uuid",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
