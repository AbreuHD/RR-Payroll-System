using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class idk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdEmpleadoProyecto",
                table: "Asistencias",
                column: "IdEmpleadoProyecto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "Asistencias",
                column: "IdEmpleadoProyecto",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_IdEmpleadoProyecto",
                table: "Asistencias");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoProyectoId",
                table: "Asistencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_EmpleadoProyectoId",
                table: "Asistencias",
                column: "EmpleadoProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_EmpleadoProyectoId",
                table: "Asistencias",
                column: "EmpleadoProyectoId",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
