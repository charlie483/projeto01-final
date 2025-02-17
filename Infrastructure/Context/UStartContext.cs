﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UStart.Domain.Entities;
using UStart.Infrastructure.Helpers;

namespace UStart.Infrastructure.Context
{

    public class UStartContext : DbContext
    {

        public UStartContext(DbContextOptions<UStartContext> options)
         : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FormaPagamento> FormasPagamentos { get; set; }        
        public DbSet<GrupoProduto> GrupoProdutos { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<CaixaItem> CaixasItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidosItens { get; set; }        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormaPagamento>(entity =>
            {
                entity.Property(forma => forma.Dias)
                .HasColumnType("jsonb");                
            });

            modelBuilder.Entity<Caixa>(entity =>
            {
                entity
                    .HasMany(or => or.Itens)
                    .WithOne(item => item.Caixa)
                    .HasForeignKey(item => item.CaixaId);
                                        
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity
                    .HasMany(ped => ped.Itens)
                    .WithOne(item => item.Pedido)
                    .HasForeignKey(item => item.PedidoId);                                        
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
