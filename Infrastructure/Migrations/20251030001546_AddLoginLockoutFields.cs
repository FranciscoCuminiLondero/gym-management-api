using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginLockoutFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempts",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LockoutEnd",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                columns: new[] { "FailedLoginAttempts", "LockoutEnd" },
                values: new object[] { 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Usuarios");
        }
    }
}
