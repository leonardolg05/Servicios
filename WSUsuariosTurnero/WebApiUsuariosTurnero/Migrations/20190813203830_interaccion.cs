using Microsoft.EntityFrameworkCore.Migrations;

namespace WSUsuariosTurnero.Migrations
{
    public partial class interaccion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "taquilla",
                table: "Usuarios",
                newName: "Taquilla");

            migrationBuilder.AddColumn<int>(
                name: "Interaccion",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interaccion",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Taquilla",
                table: "Usuarios",
                newName: "taquilla");
        }
    }
}
