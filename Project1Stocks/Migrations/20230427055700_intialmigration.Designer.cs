﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project1Stocks.Data;

#nullable disable

namespace Project1Stocks.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230427055700_intialmigration")]
    partial class intialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project1Stocks.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("StockPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Wipro",
                            StockPrice = 100m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Zensar",
                            StockPrice = 200m
                        },
                        new
                        {
                            Id = 3,
                            Name = "TCS",
                            StockPrice = 300m
                        },
                        new
                        {
                            Id = 4,
                            Name = "Tesla",
                            StockPrice = 400m
                        },
                        new
                        {
                            Id = 5,
                            Name = "Apple",
                            StockPrice = 500m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
