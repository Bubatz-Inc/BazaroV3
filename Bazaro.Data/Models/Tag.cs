using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class Tag : IdEntity
    {
        [Required]
        public TagGroup TagGroup { get; set; }
        public int TagGroupId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
