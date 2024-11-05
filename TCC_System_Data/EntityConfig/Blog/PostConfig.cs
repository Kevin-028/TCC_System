using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC_System_Domain.Blog;

namespace TCC_System_Data.EntityConfig.Blog
{
    internal class PostConfig : IEntityTypeConfiguration<Post>
    {

        public void Configure(EntityTypeBuilder<Post> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Title).HasColumnType("VARCHAR(100)");
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.Body);

            builder.Property(x => x.RecordCreatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordUpdatedBy).HasColumnType("VARCHAR(30)");
            builder.Property(x => x.RecordCreationDate).HasColumnType("DATETIME");
            builder.Property(x => x.RecordUpdateDate).HasColumnType("DATETIME");


            builder.ToTable("Blog_Post");

        }
    }
}
