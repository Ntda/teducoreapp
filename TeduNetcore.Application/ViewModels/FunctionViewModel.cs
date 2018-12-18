using System.ComponentModel.DataAnnotations;
using TeduNetcore.Data.Enums;

namespace TeduNetcore.Application.ViewModels
{
    public class FunctionViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { set; get; }

        [Required]
        [StringLength(250)]
        public string URL { set; get; }


        [StringLength(128)]
        public string ParentId { set; get; }

        public string IconCss { get; set; }
        public int SortOrder { set; get; }
        public Status Status { set; get; }

        public override string ToString()
        {
            return $"Id: {Id} - Name:{Name} - Status:{Status} - Url:{URL} - IconCss:{IconCss}";
        }
    }
}
