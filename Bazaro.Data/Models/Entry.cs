using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class Entry : IdEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Title { get; set; }
        public Item Item { get; set; }
        public int? ItemId { get; set; }
    }
}
