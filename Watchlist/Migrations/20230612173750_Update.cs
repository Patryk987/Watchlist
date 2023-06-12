using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Watch",
                table: "WatchedEpisodes",
                newName: "WatchDate");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WatchList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsWatched",
                table: "WatchedEpisodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "IsWatched",
                table: "WatchedEpisodes");

            migrationBuilder.RenameColumn(
                name: "WatchDate",
                table: "WatchedEpisodes",
                newName: "Watch");
        }
    }
}
