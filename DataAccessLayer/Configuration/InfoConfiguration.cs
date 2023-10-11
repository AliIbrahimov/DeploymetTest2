using EntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration;

public class InfoConfiguration : IEntityTypeConfiguration<Information>
{
    public void Configure(EntityTypeBuilder<Information> builder)
    {
        builder.Property(i => i.AgencyMail).HasMaxLength(255);
        builder.Property(i => i.AgencyLocation).HasMaxLength(255);
        builder.Property(i => i.AgencyPhone).HasMaxLength(255);
        builder.Property(i => i.isDeleted).HasDefaultValue(false);
    }
}
