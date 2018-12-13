using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.Entities
{
    [Table("AnnouncementUsers")]
    public class AnnouncementUser : DomainEntity<int>
    {
        [StringLength(50)]
        [Required]
        public string AnnouncementId { get; set; }

        [StringLength(50)]
        [Required]
        public Guid UserId { get; set; }

        public bool? HasRead { get; set; }

        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }
    }

}
