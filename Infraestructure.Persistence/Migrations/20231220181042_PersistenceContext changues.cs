using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersistenceContextchangues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "DetalleNomina");

            migrationBuilder.DropIndex(
                name: "IX_EmpleadoProyectos_ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "EmpleadoProyectos");

            migrationBuilder.AddColumn<decimal>(
                name: "AFP",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "INFOTEP",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ISR",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SFS",
                table: "Pagos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AFP",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "INFOTEP",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "ISR",
                table: "Pagos");

            migrationBuilder.DropColumn(
                name: "SFS",
                table: "Pagos");

            migrationBuilder.AddColumn<int>(
                name: "ContratoId",
                table: "EmpleadoProyectos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetalleNomina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProyecto = table.Column<int>(type: "int", nullable: false),
                    AFP = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SFS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SueldoBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SueldoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleNomina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleNomina_Proyectos_IdProyecto",
                        column: x => x.IdProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectos_ContratoId",
                table: "EmpleadoProyectos",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleNomina_IdProyecto",
                table: "DetalleNomina",
                column: "IdProyecto");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpleadoProyectos_Contrato_ContratoId",
                table: "EmpleadoProyectos",
                column: "ContratoId",
                principalTable: "Contrato",
                principalColumn: "Id");
        }
    }
}
