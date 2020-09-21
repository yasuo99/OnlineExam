using Microsoft.EntityFrameworkCore.Migrations;

namespace LuyenThiOnline.Migrations
{
    public partial class UpdateAccountEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Accounts");
        }
    }
}
