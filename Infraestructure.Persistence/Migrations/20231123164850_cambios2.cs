using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_IdCOntrato",
                table: "EmpleadoProyectos");

            migrationBuilder.DropIndex(
                name: "IX_EmpleadoProyectos_IdCOntrato",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "EsEncargado",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "IdCOntrato",
                table: "EmpleadoProyectos");

            migrationBuilder.AddColumn<int>(
                name: "ContratoId",
                table: "EmpleadoProyectos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectos_ContratoId",
                table: "EmpleadoProyectos",
                column: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_ContratoId",
                table: "EmpleadoProyectos",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.DropIndex(
                name: "IX_EmpleadoProyectos_ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.AddColumn<bool>(
                name: "EsEncargado",
                table: "EmpleadoProyectos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "IdCOntrato",
                table: "EmpleadoProyectos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectos_IdCOntrato",
                table: "EmpleadoProyectos",
                column: "IdCOntrato");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_IdCOntrato",
                table: "EmpleadoProyectos",
                column: "IdCOntrato",
                principalTable: "Contrato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
