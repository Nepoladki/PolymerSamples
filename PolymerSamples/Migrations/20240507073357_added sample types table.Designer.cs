﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PolymerSamples.Data;

#nullable disable

namespace PolymerSamples.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240507073357_added sample types table")]
    partial class addedsampletypestable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PolymerSamples.Models.Codes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CodeIndex")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code_index");

                    b.Property<string>("CodeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code_name");

                    b.Property<int>("Layers")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_layers");

                    b.Property<string>("LegacyCodeName")
                        .HasColumnType("text")
                        .HasColumnName("legacy_code_name");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<string>("StockLevel")
                        .HasColumnType("text")
                        .HasColumnName("stock_level");

                    b.Property<float>("Thickness")
                        .HasColumnType("real")
                        .HasColumnName("thickness");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.ToTable("codes");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodesVaults", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CodeId")
                        .HasColumnType("uuid")
                        .HasColumnName("code_id");

                    b.Property<Guid>("VaultId")
                        .HasColumnType("uuid")
                        .HasColumnName("vault_id");

                    b.HasKey("Id");

                    b.HasIndex("CodeId");

                    b.HasIndex("VaultId");

                    b.ToTable("codes_in_vaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.SampleTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type_name");

                    b.HasKey("Id");

                    b.ToTable("sample_types");
                });

            modelBuilder.Entity("PolymerSamples.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<List<string>>("Roles")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("roles");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vaults", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<string>("VaultName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("vault_name");

                    b.HasKey("Id");

                    b.ToTable("vaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodesVaults", b =>
                {
                    b.HasOne("PolymerSamples.Models.Codes", "Code")
                        .WithMany("CodeVaults")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PolymerSamples.Models.Vaults", "Vault")
                        .WithMany("CodeVaults")
                        .HasForeignKey("VaultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Code");

                    b.Navigation("Vault");
                });

            modelBuilder.Entity("PolymerSamples.Models.Codes", b =>
                {
                    b.Navigation("CodeVaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vaults", b =>
                {
                    b.Navigation("CodeVaults");
                });
#pragma warning restore 612, 618
        }
    }
}
