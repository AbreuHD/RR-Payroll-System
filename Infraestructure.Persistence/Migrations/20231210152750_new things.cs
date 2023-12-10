using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newthings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TipoDePagos_IdTipoDePago",
                table: "Pagos");

            migrationBuilder.DropTable(
                name: "TipoDePagos");

            migrationBuilder.DropColumn(
                name: "tipoPago",
                table: "Empleado");

            migrationBuilder.RenameColumn(
                name: "IdTipoDePago",
                table: "Pagos",
                newName: "IdTipoPago");

            migrationBuilder.RenameIndex(
                name: "IX_Pagos_IdTipoDePago",
                table: "Pagos",
                newName: "IX_Pagos_IdTipoPago");

            migrationBuilder.CreateTable(
                name: "TipoBanco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoBanco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoCuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCuenta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoCuenta = table.Column<int>(type: "int", nullable: false),
                    IdTipoBanco = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPago", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TipoPago_TipoBanco_IdTipoBanco",
                        column: x => x.IdTipoBanco,
                        principalTable: "TipoBanco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoPago_TipoCuenta_IdTipoCuenta",
                        column: x => x.IdTipoCuenta,
                        principalTable: "TipoCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoPago_IdTipoBanco",
                table: "TipoPago",
                column: "IdTipoBanco");

            migrationBuilder.CreateIndex(
                name: "IX_TipoPago_IdTipoCuenta",
                table: "TipoPago",
                column: "IdTipoCuenta");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TipoPago_IdTipoPago",
                table: "Pagos",
                column: "IdTipoPago",
                principalTable: "TipoPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_TipoPago_IdTipoPago",
                table: "Pagos");

            migrationBuilder.DropTable(
                name: "TipoPago");

            migrationBuilder.DropTable(
                name: "TipoBanco");

            migrationBuilder.DropTable(
                name: "TipoCuenta");

            migrationBuilder.RenameColumn(
                name: "IdTipoPago",
                table: "Pagos",
                newName: "IdTipoDePago");

            migrationBuilder.RenameIndex(
                name: "IX_Pagos_IdTipoPago",
                table: "Pagos",
                newName: "IX_Pagos_IdTipoDePago");

            migrationBuilder.AddColumn<string>(
                name: "tipoPago",
                table: "Empleado",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TipoDePagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDePagos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_TipoDePagos_IdTipoDePago",
                table: "Pagos",
                column: "IdTipoDePago",
                principalTable: "TipoDePagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
