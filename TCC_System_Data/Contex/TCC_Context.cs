using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Management;
using static System.Net.Mime.MediaTypeNames;

namespace TCC_System_Data
{
    public class TCC_Context : DbContext
    {

        public DbSet<User> Users{get; set;}
        public DbSet<Product> Products{get; set;}
        public DbSet<Module> Modules{get; set;}
        public DbSet<MessageAction> MessageActions{get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(@"data source=127.0.0.1;initial catalog=tcc_system; persist security info=False;user id=root;workstation id=127.0.0.1;packet size=4096;Connect Timeout=90;max pool size=1500");

            //optionsBuilder.UseSqlServer(@"data source=localhost\SQLEXPRESS;initial catalog=tcc_system; persist security info=False;password=Cozido#9;user id=Root;workstation id=localhost\SQLEXPRESS;packet size=4096;Connect Timeout=90;max pool size=1500");
            //optionsBuilder.UseSqlServer(@"data source=localhost\SQLEXPRESS;initial catalog=tcc_system; persist security info=False;user id=Tcc");
            optionsBuilder.UseSqlServer(@"data source=localhost\SQLEXPRESS;initial catalog=tcc_system;user id=Tcc; Trusted_Connection=True;TrustServerCertificate=True");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TCC_Context).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public int SaveChanges(string user)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.CurrentValues.Properties.Any(x => x.Name.Equals("RecordCreationDate")))
                {
                    entry.Property("RecordCreationDate").CurrentValue = DateTime.Now;
                    entry.Property("RecordCreatedBy").CurrentValue = user;
                }

                if (entry.State == EntityState.Modified && entry.CurrentValues.Properties.Any(x => x.Name.Equals("RecordUpdateDate")))
                {
                    entry.Property("RecordCreationDate").IsModified = false;
                    entry.Property("RecordCreatedBy").IsModified = false;
                    entry.Property("RecordUpdateDate").CurrentValue = DateTime.Now;
                    entry.Property("RecordUpdatedBy").CurrentValue = user;
                }
            }

            return base.SaveChanges();
        }

    }
}
