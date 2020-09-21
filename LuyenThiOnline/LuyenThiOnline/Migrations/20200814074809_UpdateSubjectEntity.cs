using Microsoft.EntityFrameworkCore.Migrations;

namespace LuyenThiOnline.Migrations
{
    public partial class UpdateSubjectEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassScore",
                table: "Subjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionPoint",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "PassScore",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "QuestionPoint",
                table: "Questions");
        }
    }
}
