using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WmiCookBook.Migrations
{
    public partial class Bonjour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

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
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "admin@gmail.com", new byte[] { 47, 174, 101, 168, 104, 136, 140, 43, 19, 85, 179, 101, 163, 62, 131, 215, 210, 254, 136, 250, 25, 62, 123, 218, 96, 201, 38, 217, 149, 210, 249, 60, 63, 204, 240, 101, 102, 42, 234, 196, 77, 213, 19, 194, 238, 72, 92, 94, 210, 87, 153, 81, 9, 235, 191, 163, 171, 88, 216, 13, 243, 207, 170, 232 }, new byte[] { 150, 15, 201, 59, 39, 104, 61, 196, 155, 235, 225, 231, 239, 251, 59, 240, 94, 58, 234, 153, 2, 76, 161, 68, 115, 173, 142, 210, 168, 121, 91, 62, 235, 193, 148, 84, 104, 240, 79, 6, 3, 192, 226, 147, 147, 145, 56, 163, 111, 157, 51, 27, 245, 88, 210, 161, 235, 68, 118, 100, 11, 166, 30, 221, 172, 249, 149, 43, 158, 171, 135, 103, 80, 213, 102, 145, 238, 1, 59, 173, 114, 71, 159, 147, 125, 67, 30, 168, 163, 14, 43, 35, 29, 56, 220, 54, 9, 42, 10, 242, 148, 132, 102, 52, 177, 57, 104, 139, 183, 247, 124, 45, 99, 53, 206, 105, 8, 230, 86, 6, 234, 156, 101, 146, 219, 234, 76, 235 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 2, "moderator@gmail.com", new byte[] { 47, 174, 101, 168, 104, 136, 140, 43, 19, 85, 179, 101, 163, 62, 131, 215, 210, 254, 136, 250, 25, 62, 123, 218, 96, 201, 38, 217, 149, 210, 249, 60, 63, 204, 240, 101, 102, 42, 234, 196, 77, 213, 19, 194, 238, 72, 92, 94, 210, 87, 153, 81, 9, 235, 191, 163, 171, 88, 216, 13, 243, 207, 170, 232 }, new byte[] { 150, 15, 201, 59, 39, 104, 61, 196, 155, 235, 225, 231, 239, 251, 59, 240, 94, 58, 234, 153, 2, 76, 161, 68, 115, 173, 142, 210, 168, 121, 91, 62, 235, 193, 148, 84, 104, 240, 79, 6, 3, 192, 226, 147, 147, 145, 56, 163, 111, 157, 51, 27, 245, 88, 210, 161, 235, 68, 118, 100, 11, 166, 30, 221, 172, 249, 149, 43, 158, 171, 135, 103, 80, 213, 102, 145, 238, 1, 59, 173, 114, 71, 159, 147, 125, 67, 30, 168, 163, 14, 43, 35, 29, 56, 220, 54, 9, 42, 10, 242, 148, 132, 102, 52, 177, 57, 104, 139, 183, 247, 124, 45, 99, 53, 206, 105, 8, 230, 86, 6, 234, 156, 101, 146, 219, 234, 76, 235 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 3, "user@gmail.com", new byte[] { 47, 174, 101, 168, 104, 136, 140, 43, 19, 85, 179, 101, 163, 62, 131, 215, 210, 254, 136, 250, 25, 62, 123, 218, 96, 201, 38, 217, 149, 210, 249, 60, 63, 204, 240, 101, 102, 42, 234, 196, 77, 213, 19, 194, 238, 72, 92, 94, 210, 87, 153, 81, 9, 235, 191, 163, 171, 88, 216, 13, 243, 207, 170, 232 }, new byte[] { 150, 15, 201, 59, 39, 104, 61, 196, 155, 235, 225, 231, 239, 251, 59, 240, 94, 58, 234, 153, 2, 76, 161, 68, 115, 173, 142, 210, 168, 121, 91, 62, 235, 193, 148, 84, 104, 240, 79, 6, 3, 192, 226, 147, 147, 145, 56, 163, 111, 157, 51, 27, 245, 88, 210, 161, 235, 68, 118, 100, 11, 166, 30, 221, 172, 249, 149, 43, 158, 171, 135, 103, 80, 213, 102, 145, 238, 1, 59, 173, 114, 71, 159, 147, 125, 67, 30, 168, 163, 14, 43, 35, 29, 56, 220, 54, 9, 42, 10, 242, 148, 132, 102, 52, 177, 57, 104, 139, 183, 247, 124, 45, 99, 53, 206, 105, 8, 230, 86, 6, 234, 156, 101, 146, 219, 234, 76, 235 } });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeId",
                table: "Steps",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
