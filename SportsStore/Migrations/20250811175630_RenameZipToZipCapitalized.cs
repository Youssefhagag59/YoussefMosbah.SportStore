using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsStore.Migrations
{
    public partial class RenameZipToZipCapitalized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Orders",
                newName: "Zip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Orders",
                newName: "zip");
        }
    }
}
