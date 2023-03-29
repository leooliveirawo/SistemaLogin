using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLogin.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class rv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "USUARIOS",
                newName: "EMAIL");

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "USUARIOS",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "USUARIOS",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "USUARIOS",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);
        }
    }
}
