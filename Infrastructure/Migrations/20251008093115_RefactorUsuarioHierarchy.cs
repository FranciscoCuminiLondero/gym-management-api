using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUsuarioHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Profesores_ProfesorId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Salas_SalaId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Sucursales_SucursalId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Alumnos_AlumnoId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Planes_PlanId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Alumnos_AlumnoId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clases_ClaseId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Sucursales_SucursalId",
                table: "Salas");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profesores",
                table: "Profesores");

            migrationBuilder.RenameTable(
                name: "Profesores",
                newName: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Usuarios",
                newName: "FechaCreacion");

            migrationBuilder.AddColumn<string>(
                name: "Especialidad",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaContratacion",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Usuarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Alumno" },
                    { 3, "Profesor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_FechaReserva_AlumnoId",
                table: "Reservas",
                columns: new[] { "FechaReserva", "AlumnoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Membresias_FechaFin_Activa_AlumnoId",
                table: "Membresias",
                columns: new[] { "FechaFin", "Activa", "AlumnoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Clases_Fecha_HoraInicio_SalaId",
                table: "Clases",
                columns: new[] { "Fecha", "HoraInicio", "SalaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Dni",
                table: "Usuarios",
                column: "Dni",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PlanId",
                table: "Usuarios",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Salas_SalaId",
                table: "Clases",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Sucursales_SucursalId",
                table: "Clases",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Usuarios_ProfesorId",
                table: "Clases",
                column: "ProfesorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Membresias_Planes_PlanId",
                table: "Membresias",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Membresias_Usuarios_AlumnoId",
                table: "Membresias",
                column: "AlumnoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clases_ClaseId",
                table: "Reservas",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_AlumnoId",
                table: "Reservas",
                column: "AlumnoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Sucursales_SucursalId",
                table: "Salas",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Planes_PlanId",
                table: "Usuarios",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Salas_SalaId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Sucursales_SucursalId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Usuarios_ProfesorId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Planes_PlanId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Membresias_Usuarios_AlumnoId",
                table: "Membresias");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clases_ClaseId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_AlumnoId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Salas_Sucursales_SucursalId",
                table: "Salas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Planes_PlanId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_FechaReserva_AlumnoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Membresias_FechaFin_Activa_AlumnoId",
                table: "Membresias");

            migrationBuilder.DropIndex(
                name: "IX_Clases_Fecha_HoraInicio_SalaId",
                table: "Clases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Dni",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PlanId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Especialidad",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaContratacion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Profesores");

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Profesores",
                newName: "Role");

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
                name: "FK_Clases_Salas_SalaId",
                table: "Clases",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Sucursales_SucursalId",
                table: "Clases",
                column: "SucursalId",
                principalTable: "Sucursales",
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
                name: "FK_Membresias_Planes_PlanId",
                table: "Membresias",
                column: "PlanId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Alumnos_AlumnoId",
                table: "Reservas",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clases_ClaseId",
                table: "Reservas",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salas_Sucursales_SucursalId",
                table: "Salas",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
