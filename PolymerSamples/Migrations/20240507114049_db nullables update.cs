using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class dbnullablesupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "codes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<float>(
                name: "thickness",
                table: "codes",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_layers",
                table: "codes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes",
                column: "type_id",
                principalTable: "sample_types",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes");

            migrationBuilder.AlterColumn<int>(
                name: "type_id",
                table: "codes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "thickness",
                table: "codes",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "number_of_layers",
                table: "codes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes",
                column: "type_id",
                principalTable: "sample_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
