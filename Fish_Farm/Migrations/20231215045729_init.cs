using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarmId",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "History",
                table: "WorkerTable");

            migrationBuilder.RenameColumn(
                name: "FishFarmId",
                table: "WorkerTable",
                newName: "FishFarm_WorkedId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerTable_FishFarmId",
                table: "WorkerTable",
                newName: "IX_WorkerTable_FishFarm_WorkedId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndedOn",
                table: "WorkerTable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedOn",
                table: "WorkerTable",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserTable",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "FishFarmTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "FishFarmTable",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FishFarmTable_ClientId",
                table: "FishFarmTable",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarm_WorkedId",
                table: "WorkerTable",
                column: "FishFarm_WorkedId",
                principalTable: "FishFarmTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FishFarmTable_ClientTable_ClientId",
                table: "FishFarmTable");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarm_WorkedId",
                table: "WorkerTable");

            migrationBuilder.DropTable(
                name: "ClientTable");

            migrationBuilder.DropIndex(
                name: "IX_FishFarmTable_ClientId",
                table: "FishFarmTable");

            migrationBuilder.DropColumn(
                name: "EndedOn",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "StartedOn",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "FishFarmTable");

            migrationBuilder.RenameColumn(
                name: "FishFarm_WorkedId",
                table: "WorkerTable",
                newName: "FishFarmId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerTable_FishFarm_WorkedId",
                table: "WorkerTable",
                newName: "IX_WorkerTable_FishFarmId");

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "WorkerTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "FishFarmTable",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarmId",
                table: "WorkerTable",
                column: "FishFarmId",
                principalTable: "FishFarmTable",
                principalColumn: "Id");
        }
    }
}
