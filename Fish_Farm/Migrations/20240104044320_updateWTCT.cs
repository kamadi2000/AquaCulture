using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class updateWTCT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "WorkerTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "FishFarmTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerTable_ClientId",
                table: "WorkerTable",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable");

            migrationBuilder.DropIndex(
                name: "IX_WorkerTable_ClientId",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "WorkerTable");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "FishFarmTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
