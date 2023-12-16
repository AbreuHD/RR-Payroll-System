using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changuesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Deducciones_IdDeducciones",
                table: "Pagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Percepciones_IdPercepciones",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdDeducciones",
                table: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Pagos_IdPercepciones",
                table: "Pagos");

            migrationBuilder.RenameColumn(
                name: "IdPercepciones",
                table: "Pagos",
                newName: "IdPago_Percepciones");

            migrationBuilder.RenameColumn(
                name: "IdDeducciones",
                table: "Pagos",
                newName: "IdPago_Deducciones");

            migrationBuilder.CreateTable(
                name: "Pago_Deducciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPago = table.Column<int>(type: "int", nullable: false),
                    IdDeducciones = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago_Deducciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Deducciones_Deducciones_IdDeducciones",
                        column: x => x.IdDeducciones,
                        principalTable: "Deducciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pago_Deducciones_Pagos_IdPago",
                        column: x => x.IdPago,
                        principalTable: "Pagos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pago_Percepciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPago = table.Column<int>(type: "int", nullable: false),
                    IdPercepciones = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago_Percepciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Percepciones_Pagos_IdPago",
                        column: x => x.IdPago,
                        principalTable: "Pagos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pago_Percepciones_Percepciones_IdPercepciones",
                        column: x => x.IdPercepciones,
                        principalTable: "Percepciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Deducciones_IdDeducciones",
                table: "Pago_Deducciones",
                column: "IdDeducciones");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Deducciones_IdPago",
                table: "Pago_Deducciones",
                column: "IdPago");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Percepciones_IdPago",
                table: "Pago_Percepciones",
                column: "IdPago");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Percepciones_IdPercepciones",
                table: "Pago_Percepciones",
                column: "IdPercepciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pago_Deducciones");

            migrationBuilder.DropTable(
                name: "Pago_Percepciones");

            migrationBuilder.RenameColumn(
                name: "IdPago_Percepciones",
                table: "Pagos",
                newName: "IdPercepciones");

            migrationBuilder.RenameColumn(
                name: "IdPago_Deducciones",
                table: "Pagos",
                newName: "IdDeducciones");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdDeducciones",
                table: "Pagos",
                column: "IdDeducciones");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IdPercepciones",
                table: "Pagos",
                column: "IdPercepciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Deducciones_IdDeducciones",
                table: "Pagos",
                column: "IdDeducciones",
                principalTable: "Deducciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Percepciones_IdPercepciones",
                table: "Pagos",
                column: "IdPercepciones",
                principalTable: "Percepciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
