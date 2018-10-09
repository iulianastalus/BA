namespace BA.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<AccountHistory> AccountHistories { get; set; }
        public virtual DbSet<AccountHolder> AccountHolders { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<AccountHolder>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.AccountHistories)
                .WithOptional(e => e.BankAccount)
                .HasForeignKey(e => e.AccountId);

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.Transactions)
                .WithOptional(e => e.BankAccount)
                .HasForeignKey(e => e.FromAccount);

            modelBuilder.Entity<BankAccount>()
                .HasMany(e => e.Transactions1)
                .WithOptional(e => e.BankAccount1)
                .HasForeignKey(e => e.ToAccount);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Rates)
                .WithRequired(e => e.Currency)
                .HasForeignKey(e => e.FromCurrency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Rates1)
                .WithRequired(e => e.Currency1)
                .HasForeignKey(e => e.ToCurrency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Transactions)
                .WithOptional(e => e.Currency1)
                .HasForeignKey(e => e.Currency);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.AccountHistories)
                .WithRequired(e => e.Transaction)
                .WillCascadeOnDelete(false);
        }
    }
}
