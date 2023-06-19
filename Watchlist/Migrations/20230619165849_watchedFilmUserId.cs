using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Migrations
{
    /// <inheritdoc />
    public partial class watchedFilmUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IMDbEmpisodesId",
                table: "WatchedEpisodes",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "IMDbEpisodesId",
                table: "WatchedEpisodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMDbEpisodesId",
                table: "WatchedEpisodes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WatchedEpisodes",
                newName: "IMDbEmpisodesId");
        }
    }
}
