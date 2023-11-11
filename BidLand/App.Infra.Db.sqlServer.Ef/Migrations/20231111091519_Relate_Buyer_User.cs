using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infra.Db.sqlServer.Ef.Migrations
{
    /// <inheritdoc />
    public partial class Relate_Buyer_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Buyers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Buyers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Buyers_UserId",
                table: "Buyers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyers_AspNetUsers_UserId",
                table: "Buyers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyers_AspNetUsers_UserId",
                table: "Buyers");

            migrationBuilder.DropIndex(
                name: "IX_Buyers_UserId",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Buyers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
