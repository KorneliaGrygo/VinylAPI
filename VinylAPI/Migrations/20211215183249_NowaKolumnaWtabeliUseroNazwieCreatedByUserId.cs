using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylAPI.Migrations
{
    public partial class NowaKolumnaWtabeliUseroNazwieCreatedByUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Bands",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Bands");
        }
    }
}
