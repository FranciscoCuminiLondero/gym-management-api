using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxReservasAndTiposPermitidosToPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Especialidad",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SucursalId",
                table: "Usuarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxReservasPorMes",
                table: "Planes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiposPermitidos",
                table: "Planes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dias",
                table: "Clases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Clases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MostrarEnHome",
                table: "Clases",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Clases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                columns: new[] { "Direccion", "Genero", "Image", "SucursalId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios",
                column: "SucursalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Sucursales_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_SucursalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "SucursalId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "MaxReservasPorMes",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "TiposPermitidos",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "Dias",
                table: "Clases");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Clases");

            migrationBuilder.DropColumn(
                name: "MostrarEnHome",
                table: "Clases");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Clases");
        }
    }
}
