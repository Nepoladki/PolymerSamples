using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class changeregistervaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_in_vaults_Vaults_vault_id",
                table: "codes_in_vaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaults",
                table: "Vaults");

            migrationBuilder.RenameTable(
                name: "Vaults",
                newName: "vaults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vaults",
                table: "vaults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_in_vaults_vaults_vault_id",
                table: "codes_in_vaults",
                column: "vault_id",
                principalTable: "vaults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_in_vaults_vaults_vault_id",
                table: "codes_in_vaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vaults",
                table: "vaults");

            migrationBuilder.RenameTable(
                name: "vaults",
                newName: "Vaults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaults",
                table: "Vaults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_in_vaults_Vaults_vault_id",
                table: "codes_in_vaults",
                column: "vault_id",
                principalTable: "Vaults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
