using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDiary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaloriesSummary = table.Column<float>(type: "real", nullable: true),
                    ProteinsSummary = table.Column<float>(type: "real", nullable: true),
                    CarbsSummary = table.Column<float>(type: "real", nullable: true),
                    FatSummary = table.Column<float>(type: "real", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserForeignKeyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDiaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodInDiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfFood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proteins = table.Column<float>(type: "real", nullable: false),
                    Carbs = table.Column<float>(type: "real", nullable: false),
                    Fat = table.Column<float>(type: "real", nullable: false),
                    UserDiaryId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodInDiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodInDiaries_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodInDiaries_UserDiaries_UserDiaryId",
                        column: x => x.UserDiaryId,
                        principalTable: "UserDiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodInDiaries_FoodId",
                table: "FoodInDiaries",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInDiaries_UserDiaryId",
                table: "FoodInDiaries",
                column: "UserDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDiaries_UserId",
                table: "UserDiaries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodInDiaries");

            migrationBuilder.DropTable(
                name: "UserDiaries");
        }
    }
}
