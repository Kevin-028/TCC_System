using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TCC_System_Domain.Arduino;

namespace TCC_System_Data.EntityConfig
{
    internal class MessageActionConfig : IEntityTypeConfiguration<MessageAction>
    {
        public void Configure(EntityTypeBuilder<MessageAction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();


            builder.Property(x => x.Type).HasConversion<string>()
                .HasColumnName("Type")
                .HasColumnType("VARCHAR(20)");   
            builder.Property(x => x.Action).HasConversion<string>()
                .HasColumnName("Action")
                .HasColumnType("VARCHAR(15)");

            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");

            builder.ToTable("MessageAction");
        }
    }
}
