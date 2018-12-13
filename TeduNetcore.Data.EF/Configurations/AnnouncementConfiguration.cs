using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Data.EF.Extensions;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Data.EF.Configurations
{
    public class AnnouncementConfiguration: DbEntityConfiguration<Announcement>
    {
        /// <summary>
        /// Configurations the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override void Configure(EntityTypeBuilder<Announcement> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
