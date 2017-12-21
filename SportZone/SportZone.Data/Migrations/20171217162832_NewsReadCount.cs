using Microsoft.EntityFrameworkCore.Migrations;

namespace SportZone.Data.Migrations
{
    public partial class NewsReadCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReadCount",
                table: "News",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadCount",
                table: "News");
        }
    }
}
