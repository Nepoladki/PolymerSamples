using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class enumtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:roles.users_roles", "user,editor,admin");

            migrationBuilder.AlterColumn<int>(
                name: "roles",
                table: "users",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:roles.users_roles", "user,editor,admin");

            migrationBuilder.AlterColumn<int>(
                name: "roles",
                table: "users",
                type: "roles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
