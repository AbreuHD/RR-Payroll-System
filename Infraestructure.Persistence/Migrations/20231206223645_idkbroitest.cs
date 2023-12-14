﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class idkbroitest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horas_Asistencias_IdAsistencia",
                table: "Horas");

            migrationBuilder.AlterColumn<int>(
                name: "IdAsistencia",
                table: "Horas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Horas_Asistencias_IdAsistencia",
                table: "Horas",
                column: "IdAsistencia",
                principalTable: "Asistencias",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horas_Asistencias_IdAsistencia",
                table: "Horas");

            migrationBuilder.AlterColumn<int>(
                name: "IdAsistencia",
                table: "Horas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Horas_Asistencias_IdAsistencia",
                table: "Horas",
                column: "IdAsistencia",
                principalTable: "Asistencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
