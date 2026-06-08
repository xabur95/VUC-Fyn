using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Semesterprojekt1PBA.DatabaseMigration.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleTypeToUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "UserRoles");
        }
    }
}
