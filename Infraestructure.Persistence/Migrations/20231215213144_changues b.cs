using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changuesb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_Asistencias_AsistenciaId",
                table: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_AsistenciaId",
                table: "Permisos");

            migrationBuilder.DropColumn(
                name: "AsistenciaId",
                table: "Permisos");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_IdAsistencia",
                table: "Permisos",
                column: "IdAsistencia");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_Asistencias_IdAsistencia",
                table: "Permisos",
                column: "IdAsistencia",
                principalTable: "Asistencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permisos_Asistencias_IdAsistencia",
                table: "Permisos");

            migrationBuilder.DropIndex(
                name: "IX_Permisos_IdAsistencia",
                table: "Permisos");

            migrationBuilder.AddColumn<int>(
                name: "AsistenciaId",
                table: "Permisos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_AsistenciaId",
                table: "Permisos",
                column: "AsistenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permisos_Asistencias_AsistenciaId",
                table: "Permisos",
                column: "AsistenciaId",
                principalTable: "Asistencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
