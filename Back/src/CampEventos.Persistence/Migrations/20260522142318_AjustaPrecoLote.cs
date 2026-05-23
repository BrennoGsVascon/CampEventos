using Microsoft.EntityFrameworkCore.Migrations;

namespace CampEventos.Persistence.Migrations
{
    public partial class AjustaPrecoLote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preço",
                table: "Lotes");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Lotes",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Lotes");

            migrationBuilder.AddColumn<string>(
                name: "Preço",
                table: "Lotes",
                type: "TEXT",
                nullable: true);
        }
    }
}
