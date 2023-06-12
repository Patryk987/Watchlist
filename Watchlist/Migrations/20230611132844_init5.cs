using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Watchlist.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IMDbId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWatch = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WatchedEpisodes",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IMDbEmpisodesId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Watch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WatchListModelid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedEpisodes", x => x.id);
                    table.ForeignKey(
                        name: "FK_WatchedEpisodes_WatchList_WatchListModelid",
                        column: x => x.WatchListModelid,
                        principalTable: "WatchList",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchedEpisodes_WatchListModelid",
                table: "WatchedEpisodes",
                column: "WatchListModelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedEpisodes");

            migrationBuilder.DropTable(
                name: "WatchList");
        }
    }
}
