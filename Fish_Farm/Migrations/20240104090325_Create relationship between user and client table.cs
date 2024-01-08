using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class Createrelationshipbetweenuserandclienttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ClientTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientTable_UserId",
                table: "ClientTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientTable_UserTable_UserId",
                table: "ClientTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientTable_UserTable_UserId",
                table: "ClientTable");

            migrationBuilder.DropIndex(
                name: "IX_ClientTable_UserId",
                table: "ClientTable");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClientTable");
        }
    }
}
