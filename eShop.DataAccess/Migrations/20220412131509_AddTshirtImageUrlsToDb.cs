using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.DataAccess.Migrations
{
    public partial class AddTshirtImageUrlsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TshirtImagesUrls",
                columns: table => new
                {
                    UrlID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TshirtID = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TshirtImagesUrls", x => x.UrlID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TshirtImagesUrls");
        }
    }
}
