using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class fistLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isTheFirsTime",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isTheFirsTime",
                schema: "Identity",
                table: "Users");
        }
    }
}
