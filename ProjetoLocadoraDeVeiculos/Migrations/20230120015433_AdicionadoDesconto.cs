using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLocadoraDeVeiculos.Migrations
{
    public partial class AdicionadoDesconto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Desconto",
                table: "Locacao",
                type: "decimal(65,30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Locacao");
        }
    }
}
