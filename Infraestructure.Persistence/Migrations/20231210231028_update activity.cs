using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateactivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actividades_ActividadesAsignadas_IdActividadAsignada",
                table: "Actividades");

            migrationBuilder.DropIndex(
                name: "IX_Actividades_IdActividadAsignada",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "IdActividadAsignada",
                table: "Actividades");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAsignadas_IdActividad",
                table: "ActividadesAsignadas",
                column: "IdActividad");

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadesAsignadas_Actividades_IdActividad",
                table: "ActividadesAsignadas",
                column: "IdActividad",
                principalTable: "Actividades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_Actividades_IdActividad",
                table: "ActividadesAsignadas");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_IdActividad",
                table: "ActividadesAsignadas");

            migrationBuilder.AddColumn<int>(
                name: "IdActividadAsignada",
                table: "Actividades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_IdActividadAsignada",
                table: "Actividades",
                column: "IdActividadAsignada");

            migrationBuilder.AddForeignKey(
                name: "FK_Actividades_ActividadesAsignadas_IdActividadAsignada",
                table: "Actividades",
                column: "IdActividadAsignada",
                principalTable: "ActividadesAsignadas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
