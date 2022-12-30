﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BankAPI.Data.BankModels;

namespace BankAPI.Data
{
    public partial class BankContext : DbContext
    {
        public BankContext()
        {
        }

        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<BankTransaction> BankTransactions { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AccountTypeNavigation)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Account__30F848ED");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Account__ClientI__31EC6D26");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<BankTransaction>(entity =>
            {
                entity.ToTable("BankTransaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankTrans__Accou__32E0915F");

                entity.HasOne(d => d.TransactionTypeNavigation)
                    .WithMany(p => p.BankTransactions)
                    .HasForeignKey(d => d.TransactionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BankTrans__Trans__33D4B598");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
