using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateguididuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Empleado_IdEmpleado",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdEmpleado",
                table: "Pagos");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "TipoPago",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Pagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_EmpleadoId",
                table: "Pagos",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Empleado_EmpleadoId",
                table: "Pagos",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Empleado_EmpleadoId",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_EmpleadoId",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "TipoPago");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Pagos");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdEmpleado",
                table: "Pagos",
                column: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Empleado_IdEmpleado",
                table: "Pagos",
                column: "IdEmpleado",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
