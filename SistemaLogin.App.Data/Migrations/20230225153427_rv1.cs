using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaLogin.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class rv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_USUARIO = table.Column<string>(type: "VARCHAR(35)", maxLength: 35, nullable: false),
                    HASH_SENHA = table.Column<string>(type: "VARCHAR(135)", maxLength: 135, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_NOME_USUARIO",
                table: "USUARIOS",
                column: "NOME_USUARIO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
