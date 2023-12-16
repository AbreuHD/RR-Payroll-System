using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Actupdates2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActividadesAsignadas_Estado_IdEstado",
                table: "ActividadesAsignadas");

            migrationBuilder.DropIndex(
                name: "IX_ActividadesAsignadas_IdEstado",
                table: "ActividadesAsignadas");

            migrationBuilder.RenameColumn(
                name: "IdEstado",
                table: "ActividadesAsignadas",
                newName: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "ActividadesAsignadas",
                newName: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_ActividadesAsignadas_IdEstado",
                table: "ActividadesAsignadas",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_ActividadesAsignadas_Estado_IdEstado",
                table: "ActividadesAsignadas",
                column: "IdEstado",
                principalTable: "Estado",
                principalColumn: "Id");
        }
    }
}
