using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC_System_Domain.Arduino;

namespace TCC_System_Data.EntityConfig
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();
            
            builder.Property(x => x.Name).HasColumnType("VARCHAR(100)");
            builder.Property(x => x.UserId);

            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");


            builder.ToTable("Produto");

        }

    }
}
