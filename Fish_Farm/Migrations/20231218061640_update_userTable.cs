using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fish_Farm.Migrations
{
    /// <inheritdoc />
    public partial class update_userTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserTable");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "UserTable",
                newName: "Birthday");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "UserTable",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "UserTable",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserTable");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "UserTable");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "UserTable",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
