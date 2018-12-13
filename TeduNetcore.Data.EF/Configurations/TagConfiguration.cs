using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.EF.Extensions;

namespace TeduNetcore.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        #region Const
        private const int MaxLengthTagId = 50;

        private const string VarChar50 = "varchar(50)";
        #endregion Const
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.Property(p => p.Id).HasMaxLength(50).IsRequired().IsUnicode(false);
        }
    }
}
