using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC_System_Domain.Management;

namespace TCC_System_Data
{
    public class TCC_Context : DbContext
    {

        public DbSet<User> Users{get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer(@"data source=127.0.0.1;initial catalog=tcc_system; persist security info=False;user id=root;workstation id=127.0.0.1;packet size=4096;Connect Timeout=90;max pool size=1500");

            optionsBuilder.UseSqlite(@"Data Source=TCC_System.db");

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
