using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHybridBackend.Migrations
{
    /// <inheritdoc />
    public partial class SmallModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Workouts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Workouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intensity",
                table: "Exercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Exercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Intensity",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Exercises");
        }
    }
}
