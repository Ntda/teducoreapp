using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduNetcore.Infrastructure.SharedKernel;

namespace TeduNetcore.Data.Entities
{
    [Table("BlogTags")]
    public class BlogTag : DomainEntity<string>
    {
        public int BlogId { set; get; }

        public string TagId { set; get; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { set; get; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { set; get; }
    }
}
