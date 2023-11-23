using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Licencias_IdEmpleado",
                table: "Licencias");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "IdLicencia",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "NumCuenta",
                table: "Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_IdEmpleado",
                table: "Licencias",
                column: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Licencias_IdEmpleado",
                table: "Licencias");

            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Empleado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Empleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdLicencia",
                table: "Empleado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NumCuenta",
                table: "Empleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_IdEmpleado",
                table: "Licencias",
                column: "IdEmpleado",
                unique: true);
        }
    }
}
