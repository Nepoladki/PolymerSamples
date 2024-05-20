﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PolymerSamples.Data;

#nullable disable

namespace PolymerSamples.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
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

                    b.Property<int?>("Layers")
                        .HasColumnType("integer")
                        .HasColumnName("layers");

                    b.Property<string>("LegacyCodeName")
                        .HasColumnType("text")
                        .HasColumnName("legacy_code_name");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("note");

                    b.Property<string>("StockLevel")
                        .HasColumnType("text")
                        .HasColumnName("stock_level");

                    b.Property<float?>("Thickness")
                        .HasColumnType("real")
                        .HasColumnName("thickness");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer")
                        .HasColumnName("type_id");

                    b.HasKey("Id")
                        .HasName("pk_codes");

                    b.HasIndex("TypeId")
                        .HasDatabaseName("ix_codes_type_id");

                    b.ToTable("codes", (string)null);
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

                    b.HasKey("Id")
                        .HasName("pk_codes_vaults");

                    b.HasIndex("CodeId")
                        .HasDatabaseName("ix_codes_vaults_code_id");

                    b.HasIndex("VaultId")
                        .HasDatabaseName("ix_codes_vaults_vault_id");

                    b.ToTable("codes_vaults", (string)null);
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

                    b.HasKey("Id")
                        .HasName("pk_sample_types");

                    b.ToTable("sample_types", (string)null);
                });

            modelBuilder.Entity("PolymerSamples.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("RefreshExpires")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_expires");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
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

                    b.HasKey("Id")
                        .HasName("pk_vaults");

                    b.ToTable("vaults", (string)null);
                });

            modelBuilder.Entity("PolymerSamples.Models.Codes", b =>
                {
                    b.HasOne("PolymerSamples.Models.SampleTypes", "SampleType")
                        .WithMany("Code")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("fk_codes_sample_types_type_id");

                    b.Navigation("SampleType");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodesVaults", b =>
                {
                    b.HasOne("PolymerSamples.Models.Codes", "Code")
                        .WithMany("CodeVaults")
                        .HasForeignKey("CodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_codes_vaults_codes_code_id");

                    b.HasOne("PolymerSamples.Models.Vaults", "Vault")
                        .WithMany("CodeVaults")
                        .HasForeignKey("VaultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_codes_vaults_vaults_vault_id");

                    b.Navigation("Code");

                    b.Navigation("Vault");
                });

            modelBuilder.Entity("PolymerSamples.Models.Codes", b =>
                {
                    b.Navigation("CodeVaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.SampleTypes", b =>
                {
                    b.Navigation("Code");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vaults", b =>
                {
                    b.Navigation("CodeVaults");
                });
#pragma warning restore 612, 618
        }
    }
}
