using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.DataAccess.Migrations
{
    public partial class RemoveColorFromTshirt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tshirts_Colors_ColorId",
                table: "Tshirts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Tshirts_ColorId",
                table: "Tshirts");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Tshirts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Tshirts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorInRGB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tshirts_ColorId",
                table: "Tshirts",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tshirts_Colors_ColorId",
                table: "Tshirts",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
