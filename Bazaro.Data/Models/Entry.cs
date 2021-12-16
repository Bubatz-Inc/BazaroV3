using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class Entry : IdEntity
    {
        [MaxLength(256)]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        public List<EntryTag> EntryTag { get; set; }
        public List<int> EntryTagId { get; set; }

        public Item? StartItem { get; set; }
        public int? StartItemId { get; set; }
    }
}
