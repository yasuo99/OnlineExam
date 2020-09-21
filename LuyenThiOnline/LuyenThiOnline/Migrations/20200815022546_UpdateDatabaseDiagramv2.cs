using Microsoft.EntityFrameworkCore.Migrations;

namespace LuyenThiOnline.Migrations
{
    public partial class UpdateDatabaseDiagramv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Accounts_AccountId",
                table: "Badges");

            migrationBuilder.DropIndex(
                name: "IX_Badges_AccountId",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Badges");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Badges",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badges_AccountId",
                table: "Badges",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Accounts_AccountId",
                table: "Badges",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
