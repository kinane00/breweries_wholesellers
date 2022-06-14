﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using breweries_wholesellers.Entity.Context;

#nullable disable

namespace breweries_wholesellers.Entity.Migrations
{
    [DbContext(typeof(breweries_wholesellersContext))]
    [Migration("20220613235459_InitailCreate")]
    partial class InitailCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("breweries_wholesellers.Entity.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("AlcoholContent")
                        .HasColumnType("real");

                    b.Property<int>("BreweryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<float>("SalePrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beer");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Brewery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Brewery");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Wholesaller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Wholasaller");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.WholesallerSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int?>("BreweryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<float>("TotalPrice")
                        .HasColumnType("real");

                    b.Property<int>("WholeSallerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("BreweryId");

                    b.HasIndex("WholeSallerId");

                    b.ToTable("WholesallerSale");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.WholesallerStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("UnitStock")
                        .HasColumnType("int");

                    b.Property<int>("WholeSallerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholeSallerId");

                    b.ToTable("WholesallerStock");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Beer", b =>
                {
                    b.HasOne("breweries_wholesellers.Entity.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.WholesallerSale", b =>
                {
                    b.HasOne("breweries_wholesellers.Entity.Beer", "Beer")
                        .WithMany("WholesallerSale")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("breweries_wholesellers.Entity.Brewery", null)
                        .WithMany("WholesallerOrder")
                        .HasForeignKey("BreweryId");

                    b.HasOne("breweries_wholesellers.Entity.Wholesaller", "Wholesaller")
                        .WithMany("WholesallerSale")
                        .HasForeignKey("WholeSallerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaller");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.WholesallerStock", b =>
                {
                    b.HasOne("breweries_wholesellers.Entity.Beer", "Beer")
                        .WithMany("WholesallerStock")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("breweries_wholesellers.Entity.Wholesaller", "Wholesaller")
                        .WithMany("WholesallerStock")
                        .HasForeignKey("WholeSallerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaller");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Beer", b =>
                {
                    b.Navigation("WholesallerSale");

                    b.Navigation("WholesallerStock");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Brewery", b =>
                {
                    b.Navigation("Beers");

                    b.Navigation("WholesallerOrder");
                });

            modelBuilder.Entity("breweries_wholesellers.Entity.Wholesaller", b =>
                {
                    b.Navigation("WholesallerSale");

                    b.Navigation("WholesallerStock");
                });
#pragma warning restore 612, 618
        }
    }
}
