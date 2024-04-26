﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockLevel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("PolymerSamples.Models.CodeVault", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VaultId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CodeId");

                    b.HasIndex("VaultId");

                    b.ToTable("CodeVaults");
                });

            modelBuilder.Entity("PolymerSamples.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PolymerSamples.Models.Vault", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Note")
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
