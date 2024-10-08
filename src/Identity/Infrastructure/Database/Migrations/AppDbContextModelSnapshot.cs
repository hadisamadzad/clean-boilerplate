﻿// <auto-generated />
using System;
using Identity.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Identity.Infrastructure.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence<int>("UserId_HiLo")
                .StartsAt(5L)
                .IncrementsBy(4);

            modelBuilder.Entity("Identity.Application.Types.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "UserId_HiLo");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("FailedLoginCount")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)");

                    b.Property<DateTime?>("LastPasswordChangeTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LockoutEndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mobile")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("SecurityStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
