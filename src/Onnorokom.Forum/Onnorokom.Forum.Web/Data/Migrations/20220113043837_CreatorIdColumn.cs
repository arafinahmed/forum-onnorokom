using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onnorokom.Forum.Web.Data.Migrations
{
    public partial class CreatorIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Topics");
        }
    }
}
