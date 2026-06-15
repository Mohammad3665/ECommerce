using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishTitleToSomeOfEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Brands",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishTitle",
                table: "Articles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "ArticleCategories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "EnglishTitle",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "ArticleCategories");
        }
    }
}
