using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class idkbroi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HorasExtras",
                table: "Horas");

            migrationBuilder.DropColumn(
                name: "HorasNormales",
                table: "Horas");

            migrationBuilder.DropColumn(
                name: "HorasTrabajadas",
                table: "Horas");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraFinal",
                table: "Horas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HorasInicio",
                table: "Horas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraFinal",
                table: "Horas");

            migrationBuilder.DropColumn(
                name: "HorasInicio",
                table: "Horas");

            migrationBuilder.AddColumn<string>(
                name: "HorasExtras",
                table: "Horas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HorasNormales",
                table: "Horas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HorasTrabajadas",
                table: "Horas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
