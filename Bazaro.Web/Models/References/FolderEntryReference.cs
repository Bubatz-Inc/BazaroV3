using Bazaro.Web.Models.Base;

namespace Bazaro.Web.Models.References
{
    public record class FolderEntryReference : IdEntity
    {
        public Folder Folder { get; set; }
        public int FolderId { get; set; }

        public Entry Entry { get; set; }
        public int EntryId { get; set; }
    }
}
