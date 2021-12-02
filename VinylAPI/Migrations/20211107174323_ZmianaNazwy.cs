using Microsoft.EntityFrameworkCore.Migrations;

namespace VinylAPI.Migrations
{
    public partial class ZmianaNazwy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_MusicAlbums_MusicAlbumId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Songs");

            migrationBuilder.AlterColumn<int>(
                name: "MusicAlbumId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_MusicAlbums_MusicAlbumId",
                table: "Songs",
                column: "MusicAlbumId",
                principalTable: "MusicAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_MusicAlbums_MusicAlbumId",
                table: "Songs");

            migrationBuilder.AlterColumn<int>(
                name: "MusicAlbumId",
                table: "Songs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_MusicAlbums_MusicAlbumId",
                table: "Songs",
                column: "MusicAlbumId",
                principalTable: "MusicAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
