using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newentityusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Profesores_ProfesorId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Alumnos_AlumnoId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Alumnos_AlumnoId",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesores",
                table: "Profesores");

            migrationBuilder.RenameTable(
                name: "Profesores",
                newName: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Apellido", "Discriminator", "Dni", "Email", "FechaNacimiento", "Nombre", "PasswordHash", "Role", "Telefono" },
                values: new object[] { -9999, true, "User", "Usuario", "00000000", "admin@gym.com", new DateOnly(1990, 1, 1), "Admin", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", "Administrador", "" });

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Usuarios_ProfesorId",
                table: "Clases",
                column: "ProfesorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membresias_Usuarios_AlumnoId",
                table: "Membresias",
                column: "AlumnoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_AlumnoId",
                table: "Reservas",
                column: "AlumnoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Usuarios_ProfesorId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Usuarios_AlumnoId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_AlumnoId",
                table: "Reservas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Profesores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profesores",
                table: "Profesores",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Profesores_ProfesorId",
                table: "Clases",
                column: "ProfesorId",
                principalTable: "Profesores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Membresias_Alumnos_AlumnoId",
                table: "Membresias",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Alumnos_AlumnoId",
                table: "Reservas",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
