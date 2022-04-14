using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmeWiki.Migrations
{
    public partial class Classificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classificacao",
                table: "Filme",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UF",
                table: "Cinema",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classificacao",
                table: "Filme");

            migrationBuilder.AlterColumn<int>(
                name: "UF",
                table: "Cinema",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
