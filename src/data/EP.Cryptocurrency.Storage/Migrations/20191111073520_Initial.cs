using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EP.Cryptocurrency.Storage.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "crypto");

            migrationBuilder.CreateTable(
                name: "Cryptocurrency",
                schema: "crypto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinMarketCapId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Symbol = table.Column<string>(maxLength: 10, nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    CirculatingSupply = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    TotalSupply = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    MaxSupply = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(25,6)", nullable: false),
                    Volume24h = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    MarketCap = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    PercentChange1h = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    PercentChange24h = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    PercentChange7d = table.Column<decimal>(type: "decimal(25,6)", nullable: true),
                    LastUpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrency", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrency_CoinMarketCapId",
                schema: "crypto",
                table: "Cryptocurrency",
                column: "CoinMarketCapId");

            migrationBuilder.CreateIndex(
                name: "IX_Cryptocurrency_Rank",
                schema: "crypto",
                table: "Cryptocurrency",
                column: "Rank");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptocurrency",
                schema: "crypto");
        }
    }
}
