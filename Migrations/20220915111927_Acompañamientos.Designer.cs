﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiRestaurante;

#nullable disable

namespace WebApiRestaurante.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220915111927_Acompañamientos")]
    partial class Acompañamientos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiRestaurante.Entidades.Acompañamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatilloId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlatilloId");

                    b.ToTable("Acompañamientos");
                });

            modelBuilder.Entity("WebApiRestaurante.Entidades.Platillos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricpion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Platillos");
                });

            modelBuilder.Entity("WebApiRestaurante.Entidades.Acompañamiento", b =>
                {
                    b.HasOne("WebApiRestaurante.Entidades.Platillos", "Platillo")
                        .WithMany("Acompañamientos")
                        .HasForeignKey("PlatilloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platillo");
                });

            modelBuilder.Entity("WebApiRestaurante.Entidades.Platillos", b =>
                {
                    b.Navigation("Acompañamientos");
                });
#pragma warning restore 612, 618
        }
    }
}
