using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WSUsuariosTurnero.Migrations
{
    public partial class interacciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaLogueo",
                table: "Usuarios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaLogueo",
                table: "Usuarios");
        }
    }
}
