using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WmiCookBook.Migrations
{
    public partial class Uzytkownicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "admin@gmail.com", new byte[] { 152, 93, 155, 139, 80, 201, 37, 106, 105, 2, 67, 139, 238, 17, 124, 253, 152, 235, 129, 78, 137, 119, 87, 103, 30, 104, 122, 208, 154, 104, 234, 200, 76, 206, 195, 157, 112, 239, 16, 73, 41, 20, 238, 245, 167, 158, 98, 12, 172, 220, 221, 182, 219, 210, 38, 79, 188, 116, 214, 205, 47, 220, 39, 121 }, new byte[] { 138, 114, 149, 134, 106, 96, 197, 152, 76, 162, 97, 20, 29, 227, 194, 238, 97, 54, 254, 165, 234, 45, 41, 10, 5, 32, 174, 103, 66, 38, 76, 60, 168, 92, 105, 127, 26, 205, 34, 233, 90, 152, 77, 72, 61, 156, 119, 63, 236, 26, 148, 212, 250, 23, 61, 242, 113, 169, 200, 180, 229, 182, 199, 233, 17, 236, 37, 146, 119, 173, 164, 141, 153, 191, 136, 38, 81, 208, 150, 89, 179, 183, 93, 51, 38, 107, 73, 92, 47, 216, 192, 188, 102, 168, 254, 236, 221, 147, 19, 169, 201, 194, 183, 115, 253, 20, 63, 82, 113, 88, 24, 195, 194, 53, 238, 159, 154, 254, 216, 67, 74, 19, 82, 107, 84, 222, 136, 74 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 2, "moderator@gmail.com", new byte[] { 152, 93, 155, 139, 80, 201, 37, 106, 105, 2, 67, 139, 238, 17, 124, 253, 152, 235, 129, 78, 137, 119, 87, 103, 30, 104, 122, 208, 154, 104, 234, 200, 76, 206, 195, 157, 112, 239, 16, 73, 41, 20, 238, 245, 167, 158, 98, 12, 172, 220, 221, 182, 219, 210, 38, 79, 188, 116, 214, 205, 47, 220, 39, 121 }, new byte[] { 138, 114, 149, 134, 106, 96, 197, 152, 76, 162, 97, 20, 29, 227, 194, 238, 97, 54, 254, 165, 234, 45, 41, 10, 5, 32, 174, 103, 66, 38, 76, 60, 168, 92, 105, 127, 26, 205, 34, 233, 90, 152, 77, 72, 61, 156, 119, 63, 236, 26, 148, 212, 250, 23, 61, 242, 113, 169, 200, 180, 229, 182, 199, 233, 17, 236, 37, 146, 119, 173, 164, 141, 153, 191, 136, 38, 81, 208, 150, 89, 179, 183, 93, 51, 38, 107, 73, 92, 47, 216, 192, 188, 102, 168, 254, 236, 221, 147, 19, 169, 201, 194, 183, 115, 253, 20, 63, 82, 113, 88, 24, 195, 194, 53, 238, 159, 154, 254, 216, 67, 74, 19, 82, 107, 84, 222, 136, 74 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 3, "user@gmail.com", new byte[] { 152, 93, 155, 139, 80, 201, 37, 106, 105, 2, 67, 139, 238, 17, 124, 253, 152, 235, 129, 78, 137, 119, 87, 103, 30, 104, 122, 208, 154, 104, 234, 200, 76, 206, 195, 157, 112, 239, 16, 73, 41, 20, 238, 245, 167, 158, 98, 12, 172, 220, 221, 182, 219, 210, 38, 79, 188, 116, 214, 205, 47, 220, 39, 121 }, new byte[] { 138, 114, 149, 134, 106, 96, 197, 152, 76, 162, 97, 20, 29, 227, 194, 238, 97, 54, 254, 165, 234, 45, 41, 10, 5, 32, 174, 103, 66, 38, 76, 60, 168, 92, 105, 127, 26, 205, 34, 233, 90, 152, 77, 72, 61, 156, 119, 63, 236, 26, 148, 212, 250, 23, 61, 242, 113, 169, 200, 180, 229, 182, 199, 233, 17, 236, 37, 146, 119, 173, 164, 141, 153, 191, 136, 38, 81, 208, 150, 89, 179, 183, 93, 51, 38, 107, 73, 92, 47, 216, 192, 188, 102, 168, 254, 236, 221, 147, 19, 169, 201, 194, 183, 115, 253, 20, 63, 82, 113, 88, 24, 195, 194, 53, 238, 159, 154, 254, 216, 67, 74, 19, 82, 107, 84, 222, 136, 74 } });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
