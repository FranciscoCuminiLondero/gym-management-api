using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newentityusuario2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: "PrP+ZrMeO00Q+nC1ytSccRIpSvauTkdqHEBRVdRaoSE=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=");
        }
    }
}
