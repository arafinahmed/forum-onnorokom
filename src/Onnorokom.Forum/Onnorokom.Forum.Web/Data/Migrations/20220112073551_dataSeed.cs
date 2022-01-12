using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onnorokom.Forum.Web.Data.Migrations
{
    public partial class dataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3005e5e8-c284-452e-88db-1be192552642"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("65f38d17-a27b-4fc2-aa39-6837144334e2"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("2a143067-3fb6-4f3f-98eb-def087eb109a"), "fa3344a1-9b56-428d-9ab4-242d26e50269", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("4ba21d17-018a-4c06-965c-632ee43c8f2d"), "6d3507f3-5a92-4f98-a140-5fc0454d4c91", "Moderator", "MODERATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a6f92de9-f845-435e-8890-283e30babfb1"), 0, "96f4bb28-1e45-487a-8fa2-f2a7819edd48", "mod@email.com", true, false, null, null, null, "AQAAAAEAACcQAAAAEHeDisxDJQVYDh9Y765ax1q8Hfw41KrKY1LGxx1iqjXli8L3CbWYx22nhtGlnKbiRA==", null, false, null, false, "mod@email.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "Moderator", "true", new Guid("a6f92de9-f845-435e-8890-283e30babfb1") });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("4ba21d17-018a-4c06-965c-632ee43c8f2d"), new Guid("a6f92de9-f845-435e-8890-283e30babfb1") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2a143067-3fb6-4f3f-98eb-def087eb109a"));

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4ba21d17-018a-4c06-965c-632ee43c8f2d"), new Guid("a6f92de9-f845-435e-8890-283e30babfb1") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4ba21d17-018a-4c06-965c-632ee43c8f2d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6f92de9-f845-435e-8890-283e30babfb1"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("3005e5e8-c284-452e-88db-1be192552642"), "a075fb88-b15c-42ff-a1f7-a29c7b8332d2", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("65f38d17-a27b-4fc2-aa39-6837144334e2"), "05f17e46-4d05-46c8-8231-bbbf479aac2b", "Moderator", "MODERATOR" });
        }
    }
}
