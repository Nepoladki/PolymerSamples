using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class changeregistervaultscolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "vaults",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "vaults",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VaultName",
                table: "vaults",
                newName: "vault_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "note",
                table: "vaults",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "vaults",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "vault_name",
                table: "vaults",
                newName: "VaultName");
        }
    }
}
