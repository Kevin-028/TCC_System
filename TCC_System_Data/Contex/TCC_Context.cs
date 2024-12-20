﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Blog;
using TCC_System_Domain.Management;

namespace TCC_System_Data
{
    public class TCC_Context : DbContext
    {

        public DbSet<User> Users{get; set;}
        public DbSet<Product> Products{get; set;}
        public DbSet<Module> Modules{get; set;}
        public DbSet<MessageAction> MessageActions{get; set;}
        public DbSet<Post> Posts{get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
            try
            {
                return base.SaveChanges();

            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
