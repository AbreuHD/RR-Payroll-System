using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixingthings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcentaje",
                table: "Deducciones");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "TipoCuenta",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "TipoBanco",
                newName: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "TipoCuenta",
                newName: "nombre");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "TipoBanco",
                newName: "nombre");

            migrationBuilder.AddColumn<decimal>(
                name: "Porcentaje",
                table: "Deducciones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
