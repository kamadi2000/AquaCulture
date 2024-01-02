using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class update_workerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarm_WorkedId",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "EndedOn",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "StartedOn",
                table: "WorkerTable");

            migrationBuilder.RenameColumn(
                name: "FishFarm_WorkedId",
                table: "WorkerTable",
                newName: "FishFarmId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerTable_FishFarm_WorkedId",
                table: "WorkerTable",
                newName: "IX_WorkerTable_FishFarmId");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "WorkerTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "WorkerTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkerTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "WorkerTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarmId",
                table: "WorkerTable",
                column: "FishFarmId",
                principalTable: "FishFarmTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarmId",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkerTable");

            migrationBuilder.DropColumn(
                name: "Picture",
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

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_FishFarmTable_FishFarm_WorkedId",
                table: "WorkerTable",
                column: "FishFarm_WorkedId",
                principalTable: "FishFarmTable",
                principalColumn: "Id");
        }
    }
}
