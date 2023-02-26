﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaLogin.App.Data;

#nullable disable

namespace SistemaLogin.App.Data.Migrations
{
    [DbContext(typeof(SistemaLoginDbContext))]
    partial class SistemaLoginDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SistemaLogin.App.Data.Entidades.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("HashSenha")
                        .IsRequired()
                        .HasMaxLength(135)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("HASH_SENHA");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("NOME_USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("NomeUsuario")
                        .IsUnique();

                    b.ToTable("USUARIOS", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
