using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduNetcore.Data.EF.Extensions;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Data.EF.Configurations
{
    public class AdvertistmentPositionConfiguration : DbEntityConfiguration<AdvertistmentPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPosition> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(50).IsRequired().IsUnicode(false);
            // etc.
        }
    }
}
