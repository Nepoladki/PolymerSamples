using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class snakecasepackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes");

            migrationBuilder.DropForeignKey(
                name: "FK_codes_in_vaults_codes_code_id",
                table: "codes_in_vaults");

            migrationBuilder.DropForeignKey(
                name: "FK_codes_in_vaults_vaults_vault_id",
                table: "codes_in_vaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vaults",
                table: "vaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sample_types",
                table: "sample_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codes",
                table: "codes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codes_in_vaults",
                table: "codes_in_vaults");

            migrationBuilder.RenameTable(
                name: "codes_in_vaults",
                newName: "codes_vaults");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "hashed_password");

            migrationBuilder.RenameColumn(
                name: "number_of_layers",
                table: "codes",
                newName: "layers");

            migrationBuilder.RenameIndex(
                name: "IX_codes_type_id",
                table: "codes",
                newName: "ix_codes_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_codes_in_vaults_vault_id",
                table: "codes_vaults",
                newName: "ix_codes_vaults_vault_id");

            migrationBuilder.RenameIndex(
                name: "IX_codes_in_vaults_code_id",
                table: "codes_vaults",
                newName: "ix_codes_vaults_code_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vaults",
                table: "vaults",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sample_types",
                table: "sample_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_codes",
                table: "codes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_codes_vaults",
                table: "codes_vaults",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_codes_sample_types_type_id",
                table: "codes",
                column: "type_id",
                principalTable: "sample_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_codes_vaults_codes_code_id",
                table: "codes_vaults",
                column: "code_id",
                principalTable: "codes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_codes_vaults_vaults_vault_id",
                table: "codes_vaults",
                column: "vault_id",
                principalTable: "vaults",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_codes_sample_types_type_id",
                table: "codes");

            migrationBuilder.DropForeignKey(
                name: "fk_codes_vaults_codes_code_id",
                table: "codes_vaults");

            migrationBuilder.DropForeignKey(
                name: "fk_codes_vaults_vaults_vault_id",
                table: "codes_vaults");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vaults",
                table: "vaults");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sample_types",
                table: "sample_types");

            migrationBuilder.DropPrimaryKey(
                name: "pk_codes",
                table: "codes");

            migrationBuilder.DropPrimaryKey(
                name: "pk_codes_vaults",
                table: "codes_vaults");

            migrationBuilder.RenameTable(
                name: "codes_vaults",
                newName: "codes_in_vaults");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "hashed_password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "layers",
                table: "codes",
                newName: "number_of_layers");

            migrationBuilder.RenameIndex(
                name: "ix_codes_type_id",
                table: "codes",
                newName: "IX_codes_type_id");

            migrationBuilder.RenameIndex(
                name: "ix_codes_vaults_vault_id",
                table: "codes_in_vaults",
                newName: "IX_codes_in_vaults_vault_id");

            migrationBuilder.RenameIndex(
                name: "ix_codes_vaults_code_id",
                table: "codes_in_vaults",
                newName: "IX_codes_in_vaults_code_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vaults",
                table: "vaults",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sample_types",
                table: "sample_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codes",
                table: "codes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codes_in_vaults",
                table: "codes_in_vaults",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_sample_types_type_id",
                table: "codes",
                column: "type_id",
                principalTable: "sample_types",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_codes_in_vaults_codes_code_id",
                table: "codes_in_vaults",
                column: "code_id",
                principalTable: "codes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_codes_in_vaults_vaults_vault_id",
                table: "codes_in_vaults",
                column: "vault_id",
                principalTable: "vaults",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
