using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration; 
public class BlogConfiguration : IEntityTypeConfiguration<Blog>

{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.Property(b => b.ArticleTitle).IsRequired();
        builder.Property(b => b.ArticleContent).IsRequired();
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
        builder.Property(b => b.isDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(b => b.CoverImage).IsRequired();

    }
}
