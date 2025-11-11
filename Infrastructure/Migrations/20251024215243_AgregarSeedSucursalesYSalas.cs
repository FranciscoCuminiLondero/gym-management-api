using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarSeedSucursalesYSalas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "Activa", "Direccion", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "Av. Principal 123", "centro@gym.com", "Sucursal Centro", "555-0001" },
                    { 2, true, "Calle Norte 456", "norte@gym.com", "Sucursal Norte", "555-0002" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: "PrP+ZrMeO00Q+nC1ytSccRIpSvauTkdqHEBRVdRaoSE=");

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "Id", "Capacidad", "Descripcion", "Nombre", "SucursalId" },
                values: new object[,]
                {
                    { 1, 20, "Sala para clases de yoga y pilates", "Sala Yoga", 1 },
                    { 2, 25, "Sala equipada con bicicletas estáticas", "Sala Spinning", 1 },
                    { 3, 30, "Sala para entrenamiento funcional", "Sala Funcional", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=");
        }
    }
}
