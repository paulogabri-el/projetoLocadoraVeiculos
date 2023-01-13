using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLocadoraDeVeiculos.Migrations
{
    public partial class ComponenteInterno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Internal",
                table: "StatusVeiculo",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Internal",
                table: "StatusLocacao",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Internal",
                table: "StatusVeiculo");

            migrationBuilder.DropColumn(
                name: "Internal",
                table: "StatusLocacao");
        }
    }
}
