using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.DataAccess.Migrations
{
    public partial class AddColorNameToTshirtImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TshirtImagesUrls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "TshirtImagesUrls");
        }
    }
}
