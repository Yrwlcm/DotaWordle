﻿// <auto-generated />
using DataParser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DotaWordle.DataAcess.Postgres.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240427155613_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.HeroEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float>("AgilityBase")
                        .HasColumnType("real");

                    b.Property<float>("AttackRange")
                        .HasColumnType("real");

                    b.Property<string>("AttackType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("Complexity")
                        .HasColumnType("smallint");

                    b.Property<int>("GameVersion")
                        .HasColumnType("integer");

                    b.Property<float>("IntelligenceBase")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PrimaryAttributeId")
                        .HasColumnType("integer");

                    b.Property<float>("StartingArmor")
                        .HasColumnType("real");

                    b.Property<float>("StartingDamageMax")
                        .HasColumnType("real");

                    b.Property<float>("StartingDamageMin")
                        .HasColumnType("real");

                    b.Property<float>("StartingMovespeed")
                        .HasColumnType("real");

                    b.Property<float>("StrengthBase")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryAttributeId");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.PrimaryAttributeEntity", b =>
                {
                    b.Property<int>("PrimaryAttributeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PrimaryAttributeId");

                    b.ToTable("PrimaryAttributes");

                    b.HasData(
                        new
                        {
                            PrimaryAttributeId = 0,
                            Name = "Strength"
                        },
                        new
                        {
                            PrimaryAttributeId = 1,
                            Name = "Agility"
                        },
                        new
                        {
                            PrimaryAttributeId = 2,
                            Name = "Intelligence"
                        },
                        new
                        {
                            PrimaryAttributeId = 3,
                            Name = "All"
                        });
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("HeroId")
                        .HasColumnType("integer");

                    b.Property<short>("Level")
                        .HasColumnType("smallint");

                    b.Property<int>("RoleTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HeroId");

                    b.HasIndex("RoleTypeId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.RoleTypeEntity", b =>
                {
                    b.Property<int>("RoleTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleTypeId");

                    b.ToTable("RoleTypes");

                    b.HasData(
                        new
                        {
                            RoleTypeId = 0,
                            Name = "Carry"
                        },
                        new
                        {
                            RoleTypeId = 1,
                            Name = "Escape"
                        },
                        new
                        {
                            RoleTypeId = 2,
                            Name = "Nuker"
                        },
                        new
                        {
                            RoleTypeId = 3,
                            Name = "Initiator"
                        },
                        new
                        {
                            RoleTypeId = 4,
                            Name = "Durable"
                        },
                        new
                        {
                            RoleTypeId = 5,
                            Name = "Disabler"
                        },
                        new
                        {
                            RoleTypeId = 6,
                            Name = "Jungler"
                        },
                        new
                        {
                            RoleTypeId = 7,
                            Name = "Support"
                        },
                        new
                        {
                            RoleTypeId = 8,
                            Name = "Pusher"
                        });
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.HeroEntity", b =>
                {
                    b.HasOne("DotaWordle.DataAcess.Postgres.Models.PrimaryAttributeEntity", "PrimaryAttribute")
                        .WithMany("Heroes")
                        .HasForeignKey("PrimaryAttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrimaryAttribute");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.RoleEntity", b =>
                {
                    b.HasOne("DotaWordle.DataAcess.Postgres.Models.HeroEntity", "Hero")
                        .WithMany("Roles")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DotaWordle.DataAcess.Postgres.Models.RoleTypeEntity", "RoleType")
                        .WithMany("Roles")
                        .HasForeignKey("RoleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("RoleType");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.HeroEntity", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.PrimaryAttributeEntity", b =>
                {
                    b.Navigation("Heroes");
                });

            modelBuilder.Entity("DotaWordle.DataAcess.Postgres.Models.RoleTypeEntity", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}