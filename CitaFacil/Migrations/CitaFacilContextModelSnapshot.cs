﻿// <auto-generated />
using System;
using CitaFacil.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CitaFacil.Migrations
{
    [DbContext(typeof(CitaFacilContext))]
    partial class CitaFacilContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CitaFacil.Models.Citas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Duracion")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Hora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cita");
                });

            modelBuilder.Entity("CitaFacil.Models.Empresas", b =>
                {
                    b.Property<int>("Id_Empresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Empresa"));

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("Id_EstadoEmpresa")
                        .HasColumnType("int");

                    b.Property<int>("Id_Rol")
                        .HasColumnType("int");

                    b.Property<long>("NIT")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime>("fecha_Registro")
                        .HasColumnType("datetime2");

                    b.Property<string>("plan")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ubicacion")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id_Empresa");

                    b.HasIndex("Id_EstadoEmpresa");

                    b.HasIndex("Id_Rol");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("CitaFacil.Models.Estado", b =>
                {
                    b.Property<int>("Id_Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Estado"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id_Estado");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("CitaFacil.Models.Fecha", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Fecha");
                });

            modelBuilder.Entity("CitaFacil.Models.Pago", b =>
                {
                    b.Property<int>("Id_Pago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Pago"));

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Metodo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.HasKey("Id_Pago");

                    b.ToTable("Pago");
                });

            modelBuilder.Entity("CitaFacil.Models.Roles", b =>
                {
                    b.Property<int>("Id_roles")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_roles"));

                    b.Property<string>("Nombre_Rol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id_roles");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("CitaFacil.Models.ServiciosNegocio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServiciosN");
                });

            modelBuilder.Entity("CitaFacil.Models.Suscripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha_Pago")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha_Vencimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Plan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suscripcion");
                });

            modelBuilder.Entity("CitaFacil.Models.Usuario", b =>
                {
                    b.Property<int>("Id_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Usuario"));

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_EstadoUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Id_Rol")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("Id_Usuario");

                    b.HasIndex("Id_EstadoUsuario");

                    b.HasIndex("Id_Rol");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("CitaFacil.Models.Empresas", b =>
                {
                    b.HasOne("CitaFacil.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("Id_EstadoEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CitaFacil.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("Id_Rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("CitaFacil.Models.Usuario", b =>
                {
                    b.HasOne("CitaFacil.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("Id_EstadoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CitaFacil.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("Id_Rol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
