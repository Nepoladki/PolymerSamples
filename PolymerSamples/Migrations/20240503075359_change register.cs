using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class changeregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeVaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Codes",
                table: "Codes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Codes",
                newName: "codes");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "users",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "codes",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "codes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StockLevel",
                table: "codes",
                newName: "stock_level");

            migrationBuilder.RenameColumn(
                name: "LegacyCodeName",
                table: "codes",
                newName: "legacy_code_name");

            migrationBuilder.RenameColumn(
                name: "CodeName",
                table: "codes",
                newName: "code_name");

            migrationBuilder.RenameColumn(
                name: "CodeIndex",
                table: "codes",
                newName: "code_index");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_codes",
                table: "codes",
                column: "id");

            migrationBuilder.CreateTable(
                name: "codes_in_vaults",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vault_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codes_in_vaults", x => x.id);
                    table.ForeignKey(
                        name: "FK_codes_in_vaults_Vaults_vault_id",
                        column: x => x.vault_id,
                        principalTable: "Vaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_codes_in_vaults_codes_code_id",
                        column: x => x.code_id,
                        principalTable: "codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_codes_in_vaults_code_id",
                table: "codes_in_vaults",
                column: "code_id");

            migrationBuilder.CreateIndex(
                name: "IX_codes_in_vaults_vault_id",
                table: "codes_in_vaults",
                column: "vault_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "codes_in_vaults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_codes",
                table: "codes");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "codes",
                newName: "Codes");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "note",
                table: "Codes",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Codes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "stock_level",
                table: "Codes",
                newName: "StockLevel");

            migrationBuilder.RenameColumn(
                name: "legacy_code_name",
                table: "Codes",
                newName: "LegacyCodeName");

            migrationBuilder.RenameColumn(
                name: "code_name",
                table: "Codes",
                newName: "CodeName");

            migrationBuilder.RenameColumn(
                name: "code_index",
                table: "Codes",
                newName: "CodeIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Codes",
                table: "Codes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CodeVaults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    VaultId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeVaults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeVaults_Codes_CodeId",
                        column: x => x.CodeId,
                        principalTable: "Codes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodeVaults_Vaults_VaultId",
                        column: x => x.VaultId,
                        principalTable: "Vaults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeVaults_CodeId",
                table: "CodeVaults",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeVaults_VaultId",
                table: "CodeVaults",
                column: "VaultId");
        }
    }
}
