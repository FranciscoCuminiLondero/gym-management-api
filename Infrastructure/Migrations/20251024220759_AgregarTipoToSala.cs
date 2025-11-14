using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoToSala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Salas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacidad", "Descripcion", "Nombre", "Tipo" },
                values: new object[] { 25, "Sala multiuso para yoga, pilates y actividades grupales", "Sala A", "Multiuso" });

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacidad", "Descripcion", "Nombre", "Tipo" },
                values: new object[] { 20, "Sala equipada con 20 bicicletas estáticas profesionales", "Sala B", "Spinning" });

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Nombre", "Tipo" },
                values: new object[] { "Espacio amplio para entrenamiento funcional y crossfit", "Sala 1", "Funcional" });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "Id", "Capacidad", "Descripcion", "Nombre", "SucursalId", "Tipo" },
                values: new object[] { 4, 40, "Sala de musculación con equipamiento completo", "Sala 2", 2, "Pesas" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Salas");

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacidad", "Descripcion", "Nombre" },
                values: new object[] { 20, "Sala para clases de yoga y pilates", "Sala Yoga" });

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacidad", "Descripcion", "Nombre" },
                values: new object[] { 25, "Sala equipada con bicicletas estáticas", "Sala Spinning" });

            migrationBuilder.UpdateData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Descripcion", "Nombre" },
                values: new object[] { "Sala para entrenamiento funcional", "Sala Funcional" });
        }
    }
}
