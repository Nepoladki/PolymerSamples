using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class otmrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_codes_type_id",
                table: "codes",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes",
                column: "type_id",
                principalTable: "sample_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes");

            migrationBuilder.DropIndex(
                name: "IX_codes_type_id",
                table: "codes");
        }
    }
}
