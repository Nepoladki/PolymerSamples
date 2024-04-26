﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolymerSamples.Migrations
{
    /// <inheritdoc />
    public partial class addusertabletriedtochangekeyofcodevaulttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeVaults",
                table: "CodeVaults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeVaults",
                table: "CodeVaults",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeVaults_VaultId",
                table: "CodeVaults",
                column: "VaultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CodeVaults",
                table: "CodeVaults");

            migrationBuilder.DropIndex(
                name: "IX_CodeVaults_VaultId",
                table: "CodeVaults");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CodeVaults",
                table: "CodeVaults",
                columns: new[] { "VaultId", "CodeId" });
        }
    }
}
