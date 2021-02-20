using Microsoft.EntityFrameworkCore.Migrations;

namespace MeeTime.Data.Migrations
{
    public partial class newparameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentUserId",
                table: "Meet",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentUserId",
                table: "Meet");
        }
    }
}
