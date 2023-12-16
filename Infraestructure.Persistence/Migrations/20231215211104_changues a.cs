using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changuesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaSolicitud",
                table: "Permisos",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "FechaEmision",
                table: "Permisos",
                newName: "FechaFinal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Permisos",
                newName: "FechaSolicitud");

            migrationBuilder.RenameColumn(
                name: "FechaFinal",
                table: "Permisos",
                newName: "FechaEmision");
        }
    }
}
