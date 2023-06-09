﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using api_viddils2.Models;

#nullable disable

namespace api_viddils2.Migrations
{
    [DbContext(typeof(Viddil_context))]
    [Migration("20230422165031_migration_second")]
    partial class migration_second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("api_viddils2.Models.User_items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("role_")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserItems");
                });

            modelBuilder.Entity("api_viddils2.Models.Viddil_item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseSerialColumn(b.Property<int>("id"));

                    b.Property<string>("directorname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("monthlybudget")
                        .HasColumnType("integer");

                    b.Property<string>("phonenumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("websiteurl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("yearlybudget")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("ViddilItems");
                });
#pragma warning restore 612, 618
        }
    }
}
