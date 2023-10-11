using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(c=>c.isDeleted).HasDefaultValue(false);
        builder.Property(c => c.Message).IsRequired();
        builder.Property(c=>c.Email).IsRequired();  
        builder.Property(c=>c.CreatedDate).HasDefaultValue(DateTime.UtcNow);
    }
}
