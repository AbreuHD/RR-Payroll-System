using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class up5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "ActividadesAsignadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_IdEmpleadoProyecto",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_IdEmpleadoProyecto",
                table: "ActividadesAsignadas");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoProyectoId",
                table: "Asistencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoProyectoId",
                table: "ActividadesAsignadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_EmpleadoProyectoId",
                table: "Asistencias",
                column: "EmpleadoProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAsignadas_EmpleadoProyectoId",
                table: "ActividadesAsignadas",
                column: "EmpleadoProyectoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_EmpleadoProyectoId",
                table: "ActividadesAsignadas",
                column: "EmpleadoProyectoId",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_EmpleadoProyectoId",
                table: "Asistencias",
                column: "EmpleadoProyectoId",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.DropForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_Asistencias_EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.DropColumn(
                name: "EmpleadoProyectoId",
                table: "Asistencias");

            migrationBuilder.DropColumn(
                name: "EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdEmpleadoProyecto",
                table: "Asistencias",
                column: "IdEmpleadoProyecto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAsignadas_IdEmpleadoProyecto",
                table: "ActividadesAsignadas",
                column: "IdEmpleadoProyecto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "ActividadesAsignadas",
                column: "IdEmpleadoProyecto",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asistencias_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "Asistencias",
                column: "IdEmpleadoProyecto",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
