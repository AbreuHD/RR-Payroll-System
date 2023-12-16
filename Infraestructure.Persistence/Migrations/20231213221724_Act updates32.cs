using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Actupdates32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.DropColumn(
                name: "EmpleadoProyectoId",
                table: "ActividadesAsignadas");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAsignadas_IdEmpleadoProyecto",
                table: "ActividadesAsignadas",
                column: "IdEmpleadoProyecto");

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "ActividadesAsignadas",
                column: "IdEmpleadoProyecto",
                principalTable: "EmpleadoProyectos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_EmpleadoProyectos_IdEmpleadoProyecto",
                table: "ActividadesAsignadas");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_IdEmpleadoProyecto",
                table: "ActividadesAsignadas");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoProyectoId",
                table: "ActividadesAsignadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
