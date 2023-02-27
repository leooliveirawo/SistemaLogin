using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLogin.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class rv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "USUARIOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HashChaveMestre",
                table: "USUARIOS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "HashChaveMestre",
                table: "USUARIOS");
        }
    }
}
