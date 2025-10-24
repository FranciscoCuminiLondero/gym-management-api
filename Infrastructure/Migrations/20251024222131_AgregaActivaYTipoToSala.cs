using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregaActivaYTipoToSala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Salas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Activa",
                value: true);

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 4,
                column: "Activa",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Salas");
        }
    }
}
