using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    public record class Entry : IdEntity
    {
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public List<EntryTag> EntryTag { get; set; }

        public Item StartItem { get; set; }
        public int? StartItemId { get; set; }
    }
}
