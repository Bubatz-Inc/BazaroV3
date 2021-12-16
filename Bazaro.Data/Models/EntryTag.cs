using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class EntryTag : IdEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
