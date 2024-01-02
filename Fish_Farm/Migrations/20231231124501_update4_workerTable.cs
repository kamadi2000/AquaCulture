using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class update4_workerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "WorkerTable",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "WorkerTable",
                newName: "Picture");
        }
    }
}
