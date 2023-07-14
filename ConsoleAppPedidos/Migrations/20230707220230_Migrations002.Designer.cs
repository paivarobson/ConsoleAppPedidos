﻿// <auto-generated />
using ConsoleAppPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleAppPedidos.Migrations
{
    [DbContext(typeof(AppDbContexto))]
    [Migration("20230707220230_Migrations002")]
    partial class Migrations002
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsoleAppPedidos.Models.ItensDePedido", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("PedidoID")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoID")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("ID");

                    b.ToTable("ItensDePedido");
                });

            modelBuilder.Entity("ConsoleAppPedidos.Models.Pedido", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Descricao")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(21,2)");

                    b.HasKey("ID");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ConsoleAppPedidos.Models.Produto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<byte>("Categoria")
                        .HasColumnType("tinyint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
