using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    public record class Folder : IdEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        public Folder PreviousFolder { get; set; }
        public int? PreviousFolderId { get; set; }

        public List<Folder> SubFolder { get; set; }
        public List<int> SubFolderId { get; set; }
    }
}
