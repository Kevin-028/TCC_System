using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TCC_System_Domain.Management;

namespace TCC_System_Data.EntityConfig.Management.User
{
    public class ClaimConfig : IEntityTypeConfiguration<Claims>
    {
        public void Configure(EntityTypeBuilder<Claims> builder)
        {
            builder.HasKey(x => x.ClaimID);


            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");

            builder.ToTable("AUTH_Claims");
        }
    }
}
