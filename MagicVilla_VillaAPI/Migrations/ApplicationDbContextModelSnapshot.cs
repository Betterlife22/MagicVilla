﻿// <auto-generated />
using System;
using MagicVilla_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("occupancy")
                        .HasColumnType("int");

                    b.Property<double>("rate")
                        .HasColumnType("float");

                    b.Property<int>("sqft")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            id = 1,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            amenity = "",
                            createDate = new DateTime(2024, 5, 21, 8, 23, 4, 615, DateTimeKind.Local).AddTicks(6927),
                            details = "Villa xin xo",
                            imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            name = "Royal Villa",
                            occupancy = 1,
                            rate = 200.0,
                            sqft = 200
                        },
                        new
                        {
                            id = 2,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            amenity = "",
                            createDate = new DateTime(2024, 5, 21, 8, 23, 4, 615, DateTimeKind.Local).AddTicks(6939),
                            details = "Villa nha giao",
                            imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            name = "Luxury Villa",
                            occupancy = 2,
                            rate = 100.0,
                            sqft = 100
                        },
                        new
                        {
                            id = 3,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            amenity = "",
                            createDate = new DateTime(2024, 5, 21, 8, 23, 4, 615, DateTimeKind.Local).AddTicks(6941),
                            details = "Villa standard",
                            imageUrl = "https://plus.unsplash.com/premium_photo-1715876234545-88509db72eb3?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                            name = "Normal Villa",
                            occupancy = 3,
                            rate = 300.0,
                            sqft = 300
                        });
                });
#pragma warning restore 612, 618
        }
    }
}