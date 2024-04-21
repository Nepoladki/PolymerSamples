﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PolymerSamples.Data;

#nullable disable

namespace PolymerSamples.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240420114652_firstone")]
    partial class firstone
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PolymerSamples.Models.Code", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeIndex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LegacyCodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodeVault", b =>
                {
                    b.Property<Guid>("VaultId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VaultId", "CodeId");

                    b.HasIndex("CodeId");

                    b.ToTable("CodeVaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vault", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VaultName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodeVault", b =>
                {
                    b.HasOne("PolymerSamples.Models.Code", "Code")
                        .WithMany("CodeVaults")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolymerSamples.Models.Vault", "Vault")
                        .WithMany("CodeVaults")
                        .HasForeignKey("VaultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Code");

                    b.Navigation("Vault");
                });

            modelBuilder.Entity("PolymerSamples.Models.Code", b =>
                {
                    b.Navigation("CodeVaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vault", b =>
                {
                    b.Navigation("CodeVaults");
                });
#pragma warning restore 612, 618
        }
    }
}
