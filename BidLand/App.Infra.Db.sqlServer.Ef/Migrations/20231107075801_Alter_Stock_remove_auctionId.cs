using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Db.sqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Stock_remove_auctionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "Stocks");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_StockId",
                table: "Auctions",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Stocks_StockId",
                table: "Auctions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Stocks_StockId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_StockId",
                table: "Auctions");

            migrationBuilder.AddColumn<int>(
                name: "AuctionId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
