using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Db.sqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class change_medal_seller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medals_Sellers_SellerId",
                table: "Medals");

            migrationBuilder.DropIndex(
                name: "IX_Medals_SellerId",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Medals");

            migrationBuilder.AddColumn<int>(
                name: "MedalId",
                table: "Sellers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_MedalId",
                table: "Sellers",
                column: "MedalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Medals_MedalId",
                table: "Sellers",
                column: "MedalId",
                principalTable: "Medals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Medals_MedalId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_MedalId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "MedalId",
                table: "Sellers");

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Medals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medals_SellerId",
                table: "Medals",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medals_Sellers_SellerId",
                table: "Medals",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id");
        }
    }
}
