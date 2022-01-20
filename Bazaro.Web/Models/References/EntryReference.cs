using Bazaro.Web.Models.Base;

namespace Bazaro.Web.Models.References
{
    public class EntryReference : IEntity
    {
        public Entry Entry { get; set; }
        public int EntryId { get; set; }

        public Entry ReferenceEntry { get; set; }
        public int ReferenceEntryId { get; set; }
    }
}
