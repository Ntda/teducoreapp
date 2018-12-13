using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduNetcore.Data.Enums;
using TeduNetcore.Data.Interfaces;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.Entities
{
    [Table("Advertistments")]
    public class Advertistment : DomainEntity<int>, ISwitchAble, ISortAble
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(50)]
        public string PositionId { get; set; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public int SortOrder { set; get; }

        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { get; set; }
    }
}
