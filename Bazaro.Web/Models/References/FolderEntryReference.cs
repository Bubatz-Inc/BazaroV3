using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models.References
{
    public record class FolderEntryReference : IEntity
    {
        [Required]
        public Folder? Folder { get; set; }
        public int? FolderId { get; set; }

        [Required]
        public Entry Entry { get; set; }
        public int EntryId { get; set; }
    }
}
