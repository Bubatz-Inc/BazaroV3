using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    // TODO: Create n:m to Entry
    public record class EntryTag : IdEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
