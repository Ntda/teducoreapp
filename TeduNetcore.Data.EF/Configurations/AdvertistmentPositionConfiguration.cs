using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduNetcore.Data.EF.Extensions;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Data.EF.Configurations
{
    public class AdvertistmentPositionConfiguration : DbEntityConfiguration<AdvertistmentPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPosition> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(20).IsRequired();
            // etc.
        }
    }
}
