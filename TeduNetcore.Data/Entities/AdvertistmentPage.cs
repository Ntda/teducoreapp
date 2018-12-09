﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.Entities
{
    [Table("AdvertistmentPages")]
    public class AdvertistmentPage : DomainEntity<string>
    {
        public string Name { get; set; }

        public virtual ICollection<AdvertistmentPosition> AdvertistmentPositions { get; set; }
    }
}
