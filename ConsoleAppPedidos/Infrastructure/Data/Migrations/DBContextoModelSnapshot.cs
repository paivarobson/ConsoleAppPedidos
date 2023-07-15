﻿// <auto-generated />
using ConsoleAppPedidos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleAppPedidos.Migrations
{
    [DbContext(typeof(AppDbContexto))]
    partial class DBContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ConsoleAppPedidos.Models.ItemDoPedido", b =>
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

                    b.HasIndex("PedidoID");

                    b.HasIndex("ProdutoID");

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

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ConsoleAppPedidos.Models.ItemDoPedido", b =>
                {
                    b.HasOne("ConsoleAppPedidos.Models.Pedido", "Pedido")
                        .WithMany("ItensDoPedido")
                        .HasForeignKey("PedidoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleAppPedidos.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("ConsoleAppPedidos.Models.Pedido", b =>
                {
                    b.Navigation("ItensDoPedido");
                });
#pragma warning restore 612, 618
        }
    }
}
