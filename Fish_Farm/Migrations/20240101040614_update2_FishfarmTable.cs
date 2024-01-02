using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class update2_FishfarmTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "FishFarmTable",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "FishFarmTable",
                newName: "Image");
        }
    }
}
