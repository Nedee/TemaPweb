using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIDS_Items_ItemId",
                table: "BIDS");

            migrationBuilder.DropForeignKey(
                name: "FK_BIDS_User_UserId",
                table: "BIDS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BIDS",
                table: "BIDS");

            migrationBuilder.RenameTable(
                name: "BIDS",
                newName: "Bids");

            migrationBuilder.RenameIndex(
                name: "IX_BIDS_UserId",
                table: "Bids",
                newName: "IX_Bids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BIDS_ItemId",
                table: "Bids",
                newName: "IX_Bids_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Items_ItemId",
                table: "Bids",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_User_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Items_ItemId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_User_UserId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "BIDS");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_UserId",
                table: "BIDS",
                newName: "IX_BIDS_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ItemId",
                table: "BIDS",
                newName: "IX_BIDS_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BIDS",
                table: "BIDS",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIDS_Items_ItemId",
                table: "BIDS",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BIDS_User_UserId",
                table: "BIDS",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
