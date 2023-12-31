﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class updateWTCTV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "WorkerTable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "WorkerTable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerTable_ClientTable_ClientId",
                table: "WorkerTable",
                column: "ClientId",
                principalTable: "ClientTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
