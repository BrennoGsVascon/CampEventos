using Microsoft.EntityFrameworkCore.Migrations;

namespace CampEventos.Persistence.Migrations
{
    public partial class AddDescricaoModalidadeEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Apresentadores_ApresentadorId",
                table: "RedesSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Eventos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modalidade",
                table: "Eventos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Apresentadores_ApresentadorId",
                table: "RedesSociais",
                column: "ApresentadorId",
                principalTable: "Apresentadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Apresentadores_ApresentadorId",
                table: "RedesSociais");

            migrationBuilder.DropForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Modalidade",
                table: "Eventos");

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Apresentadores_ApresentadorId",
                table: "RedesSociais",
                column: "ApresentadorId",
                principalTable: "Apresentadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RedesSociais_Eventos_EventoId",
                table: "RedesSociais",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
