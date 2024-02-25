using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedCaloriesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Calories",
                table: "FoodInDiaries",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "FoodInDiaries");
        }
    }
}
