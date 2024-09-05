using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC_System_Domain.Management;

namespace TCC_System_Data.EntityConfig
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("VARCHAR(200)");
            builder.Property(x => x.GroupName).HasColumnType("VARCHAR(200)");

            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");

            builder.ToTable("AUTH_Users");
        }
    }
}
