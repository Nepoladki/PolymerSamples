using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class workinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sample_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sample_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    hashed_password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: true),
                    refresh_expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vaults",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vault_name = table.Column<string>(type: "text", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaults", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "codes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code_index = table.Column<string>(type: "text", nullable: false),
                    code_name = table.Column<string>(type: "text", nullable: false),
                    legacy_code_name = table.Column<string>(type: "text", nullable: true),
                    stock_level = table.Column<string>(type: "text", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    type_id = table.Column<int>(type: "integer", nullable: true),
                    layers = table.Column<int>(type: "integer", nullable: true),
                    thickness = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_codes", x => x.id);
                    table.ForeignKey(
                        name: "fk_codes_sample_types_type_id",
                        column: x => x.type_id,
                        principalTable: "sample_types",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "codes_vaults",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vault_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_codes_vaults", x => x.id);
                    table.ForeignKey(
                        name: "fk_codes_vaults_codes_code_id",
                        column: x => x.code_id,
                        principalTable: "codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_codes_vaults_vaults_vault_id",
                        column: x => x.vault_id,
                        principalTable: "vaults",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_codes_type_id",
                table: "codes",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "ix_codes_vaults_code_id",
                table: "codes_vaults",
                column: "code_id");

            migrationBuilder.CreateIndex(
                name: "ix_codes_vaults_vault_id",
                table: "codes_vaults",
                column: "vault_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "codes_vaults");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "codes");

            migrationBuilder.DropTable(
                name: "vaults");

            migrationBuilder.DropTable(
                name: "sample_types");
        }
    }
}
