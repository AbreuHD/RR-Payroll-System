using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambios3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empleado_IdProvincia",
                table: "Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdProvincia",
                table: "Empleado",
                column: "IdProvincia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empleado_IdProvincia",
                table: "Empleado");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_IdProvincia",
                table: "Empleado",
                column: "IdProvincia",
                unique: true);
        }
    }
}
