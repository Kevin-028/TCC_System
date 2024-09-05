using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC_System_Domain.Management;

namespace TCC_System_Data.EntityConfig.Management.User
{
    public class UserClaimsConfig : IEntityTypeConfiguration<UserClaims>
    {
        public void Configure(EntityTypeBuilder<UserClaims> builder)
        {
            builder.HasKey(x => new { x.UsuarioID, x.ClaimID });

            builder.HasOne(x => x.User)
                .WithMany(s => s.UserClaims)
                .HasForeignKey(k => k.UsuarioID); 
            builder.HasOne(x => x.Claims)
                .WithMany(s => s.ClaimUsers)
                .HasForeignKey(k => k.ClaimID);

            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");

            builder.ToTable("AUTH_UserClaims");
        }

    }
}
