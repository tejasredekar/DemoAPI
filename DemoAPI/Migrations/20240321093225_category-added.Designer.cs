﻿// <auto-generated />
using System;
using DemoAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DemoAPI.Migrations
{
    [DbContext(typeof(DemoAPIContext))]
    [Migration("20240321093225_category-added")]
    partial class categoryadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DemoAPI.Model.Category", b =>
                {
                    b.Property<int>("DID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DID"));

                    b.Property<int>("DCapacity")
                        .HasColumnType("int");

                    b.Property<string>("DName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DemoAPI.Model.Product", b =>
                {
                    b.Property<int>("PID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PID"));

                    b.Property<int?>("CateId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryDID")
                        .HasColumnType("int");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("PID");

                    b.HasIndex("CategoryDID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DemoAPI.Model.Product", b =>
                {
                    b.HasOne("DemoAPI.Model.Category", null)
                        .WithMany("Product")
                        .HasForeignKey("CategoryDID");
                });

            modelBuilder.Entity("DemoAPI.Model.Category", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
