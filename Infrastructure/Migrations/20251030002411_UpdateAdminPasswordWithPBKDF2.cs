using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminPasswordWithPBKDF2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: GeneratePBKDF2Hash("Admin123!")
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir al hash SHA256 original si es necesario
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: -9999,
                column: "PasswordHash",
                value: "PrP+ZrMeO00Q+nC1ytSccRIpSvauTkdqHEBRVdRaoSE="
            );
        }

        private static string GeneratePBKDF2Hash(string password)
        {
            // Generar salt aleatorio de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Generar hash usando PBKDF2 con 10000 iteraciones
            var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(
                password,
                salt,
                10000,
                System.Security.Cryptography.HashAlgorithmName.SHA256
            );
            byte[] hash = pbkdf2.GetBytes(32);

            // Combinar salt + hash para almacenar
            byte[] hashBytes = new byte[48]; // 16 bytes salt + 32 bytes hash
            System.Array.Copy(salt, 0, hashBytes, 0, 16);
            System.Array.Copy(hash, 0, hashBytes, 16, 32);

            return System.Convert.ToBase64String(hashBytes);
        }
    }
}
