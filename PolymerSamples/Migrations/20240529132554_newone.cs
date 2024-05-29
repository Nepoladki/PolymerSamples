using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class newone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code_index",
                table: "codes",
                newName: "short_code_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "short_code_name",
                table: "codes",
                newName: "code_index");
        }
    }
}
