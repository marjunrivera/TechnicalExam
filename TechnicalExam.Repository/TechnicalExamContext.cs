using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TechnicalExam.Repository.Entity;

namespace TechnicalExam.Repository
{
    public class TechnicalExamContext : DbContext
    {
        public TechnicalExamContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Username).IsRequired();
                e.HasIndex(x => x.Username).IsUnique();
                e.Property(x => x.InitialBalance).IsRequired();
                e.Property(x => x.Timestamp).ValueGeneratedOnAddOrUpdate()
                   .IsConcurrencyToken();
            });

            modelBuilder.Entity<Transactions>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.SourceAccountId).IsRequired();
                e.Property(x => x.DestinationAccountId).IsRequired();
                e.Property(x => x.TransferAmount).IsRequired();
                e.Property(x => x.Timestamp).ValueGeneratedOnAddOrUpdate()
                   .IsConcurrencyToken();
            });
        }
    }
}
