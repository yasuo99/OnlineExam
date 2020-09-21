using Microsoft.EntityFrameworkCore.Migrations;

namespace LuyenThiOnline.Migrations
{
    public partial class UpdateAccountBadgeDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBadges_Accounts_AccountId",
                table: "AccountBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBadges_Badges_BadgeId",
                table: "AccountBadges");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBadges_Accounts_AccountId",
                table: "AccountBadges",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBadges_Badges_BadgeId",
                table: "AccountBadges",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBadges_Accounts_AccountId",
                table: "AccountBadges");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBadges_Badges_BadgeId",
                table: "AccountBadges");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBadges_Accounts_AccountId",
                table: "AccountBadges",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBadges_Badges_BadgeId",
                table: "AccountBadges",
                column: "BadgeId",
                principalTable: "Badges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
