using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportZone.Data.Migrations
{
    public partial class TagsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsTag",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    NewsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTag", x => new { x.TagId, x.NewsId });
                    table.ForeignKey(
                        name: "FK_NewsTag_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsTag_NewsId",
                table: "NewsTag",
                column: "NewsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTag");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
