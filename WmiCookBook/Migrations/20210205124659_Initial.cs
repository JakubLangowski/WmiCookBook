using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WmiCookBook.Migrations
{
    public partial class Initial : Migration
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
                values: new object[] { 1, "admin@gmail.com", new byte[] { 105, 97, 106, 158, 214, 35, 146, 117, 156, 235, 142, 97, 121, 62, 47, 235, 50, 66, 106, 174, 163, 121, 74, 161, 230, 156, 116, 64, 180, 37, 75, 7, 5, 91, 154, 245, 105, 12, 20, 117, 194, 138, 155, 210, 101, 42, 129, 202, 191, 31, 241, 67, 162, 39, 152, 58, 134, 56, 165, 57, 141, 38, 44, 159 }, new byte[] { 76, 41, 30, 95, 20, 173, 59, 106, 135, 126, 51, 241, 3, 1, 48, 168, 98, 127, 226, 135, 3, 17, 35, 124, 90, 169, 28, 106, 207, 25, 70, 194, 120, 62, 2, 233, 177, 88, 178, 10, 158, 222, 77, 93, 66, 172, 117, 139, 204, 118, 148, 104, 137, 232, 148, 46, 197, 134, 60, 35, 169, 122, 237, 134, 221, 13, 215, 248, 252, 85, 236, 103, 31, 66, 252, 129, 228, 108, 191, 118, 142, 125, 224, 48, 243, 187, 202, 184, 47, 115, 139, 140, 82, 116, 5, 148, 102, 30, 31, 216, 8, 227, 243, 249, 37, 197, 160, 46, 151, 86, 214, 236, 94, 51, 41, 159, 5, 219, 208, 212, 150, 237, 180, 154, 181, 92, 122, 45 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 2, "moderator@gmail.com", new byte[] { 105, 97, 106, 158, 214, 35, 146, 117, 156, 235, 142, 97, 121, 62, 47, 235, 50, 66, 106, 174, 163, 121, 74, 161, 230, 156, 116, 64, 180, 37, 75, 7, 5, 91, 154, 245, 105, 12, 20, 117, 194, 138, 155, 210, 101, 42, 129, 202, 191, 31, 241, 67, 162, 39, 152, 58, 134, 56, 165, 57, 141, 38, 44, 159 }, new byte[] { 76, 41, 30, 95, 20, 173, 59, 106, 135, 126, 51, 241, 3, 1, 48, 168, 98, 127, 226, 135, 3, 17, 35, 124, 90, 169, 28, 106, 207, 25, 70, 194, 120, 62, 2, 233, 177, 88, 178, 10, 158, 222, 77, 93, 66, 172, 117, 139, 204, 118, 148, 104, 137, 232, 148, 46, 197, 134, 60, 35, 169, 122, 237, 134, 221, 13, 215, 248, 252, 85, 236, 103, 31, 66, 252, 129, 228, 108, 191, 118, 142, 125, 224, 48, 243, 187, 202, 184, 47, 115, 139, 140, 82, 116, 5, 148, 102, 30, 31, 216, 8, 227, 243, 249, 37, 197, 160, 46, 151, 86, 214, 236, 94, 51, 41, 159, 5, 219, 208, 212, 150, 237, 180, 154, 181, 92, 122, 45 } });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { 3, "user@gmail.com", new byte[] { 105, 97, 106, 158, 214, 35, 146, 117, 156, 235, 142, 97, 121, 62, 47, 235, 50, 66, 106, 174, 163, 121, 74, 161, 230, 156, 116, 64, 180, 37, 75, 7, 5, 91, 154, 245, 105, 12, 20, 117, 194, 138, 155, 210, 101, 42, 129, 202, 191, 31, 241, 67, 162, 39, 152, 58, 134, 56, 165, 57, 141, 38, 44, 159 }, new byte[] { 76, 41, 30, 95, 20, 173, 59, 106, 135, 126, 51, 241, 3, 1, 48, 168, 98, 127, 226, 135, 3, 17, 35, 124, 90, 169, 28, 106, 207, 25, 70, 194, 120, 62, 2, 233, 177, 88, 178, 10, 158, 222, 77, 93, 66, 172, 117, 139, 204, 118, 148, 104, 137, 232, 148, 46, 197, 134, 60, 35, 169, 122, 237, 134, 221, 13, 215, 248, 252, 85, 236, 103, 31, 66, 252, 129, 228, 108, 191, 118, 142, 125, 224, 48, 243, 187, 202, 184, 47, 115, 139, 140, 82, 116, 5, 148, 102, 30, 31, 216, 8, 227, 243, 249, 37, 197, 160, 46, 151, 86, 214, 236, 94, 51, 41, 159, 5, 219, 208, 212, 150, 237, 180, 154, 181, 92, 122, 45 } });

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
