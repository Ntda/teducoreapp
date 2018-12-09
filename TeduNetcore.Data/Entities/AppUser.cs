using System;
using System.ComponentModel.DataAnnotations.Schema;
using TeduNetcore.Data.Enums;
using TeduNetcore.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TeduNetcore.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchAble
    {
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
    }
}
