using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SportZone.Data.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_News_NewsId",
                table: "NewsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "NewsTags",
                newName: "NewsTag");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTags_NewsId",
                table: "NewsTag",
                newName: "IX_NewsTag_NewsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTag",
                table: "NewsTag",
                columns: new[] { "TagId", "NewsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTag_News_NewsId",
                table: "NewsTag",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTag_Tag_TagId",
                table: "NewsTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsTag_News_NewsId",
                table: "NewsTag");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTag_Tag_TagId",
                table: "NewsTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTag",
                table: "NewsTag");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "NewsTag",
                newName: "NewsTags");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTag_NewsId",
                table: "NewsTags",
                newName: "IX_NewsTags_NewsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags",
                columns: new[] { "TagId", "NewsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_News_NewsId",
                table: "NewsTags",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
