﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiRestaurante2;

#nullable disable

namespace WebApiRestaurante2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221107023654_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Acompañamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(125)
                        .HasColumnType("nvarchar(125)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Acompañamientos");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Empleados", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TurnosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurnosId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.PlatilloAcompañamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AcompañamientoId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.Property<int>("PlatilloId")
                        .HasColumnType("int");

                    b.Property<int?>("PlatillosId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AcompañamientoId");

                    b.HasIndex("PlatillosId");

                    b.ToTable("PlatilloAcompañamiento");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Platillos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricpion")
                        .IsRequired()
                        .HasMaxLength(125)
                        .HasColumnType("nvarchar(125)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Platillos");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Turnos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Entrada")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Salida")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Turnos");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Empleados", b =>
                {
                    b.HasOne("WebApiRestaurante2.Entidades.Turnos", "Turno")
                        .WithMany("Empleados")
                        .HasForeignKey("TurnosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turno");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.PlatilloAcompañamiento", b =>
                {
                    b.HasOne("WebApiRestaurante2.Entidades.Acompañamiento", "Acompañamiento")
                        .WithMany("PlatilloAcompañamiento")
                        .HasForeignKey("AcompañamientoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiRestaurante2.Entidades.Platillos", "Platillos")
                        .WithMany("PlatilloAcompañamientos")
                        .HasForeignKey("PlatillosId");

                    b.Navigation("Acompañamiento");

                    b.Navigation("Platillos");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Acompañamiento", b =>
                {
                    b.Navigation("PlatilloAcompañamiento");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Platillos", b =>
                {
                    b.Navigation("PlatilloAcompañamientos");
                });

            modelBuilder.Entity("WebApiRestaurante2.Entidades.Turnos", b =>
                {
                    b.Navigation("Empleados");
                });
#pragma warning restore 612, 618
        }
    }
}
