using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class empleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Percepciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Horas",
                table: "EmpleadoProyectos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PagoHoras",
                table: "EmpleadoProyectos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Deducciones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Percepciones");

            migrationBuilder.DropColumn(
                name: "Horas",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "PagoHoras",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Deducciones");
        }
    }
}
