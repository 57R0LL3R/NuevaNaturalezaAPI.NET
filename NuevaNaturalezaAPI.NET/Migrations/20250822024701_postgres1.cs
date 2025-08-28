using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NuevaNaturalezaAPI.NET.Migrations
{
    /// <inheritdoc />
    public partial class postgres1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Off",
                table: "Actuador",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "On",
                table: "Actuador",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Off",
                table: "Actuador");

            migrationBuilder.DropColumn(
                name: "On",
                table: "Actuador");
        }
    }
}
