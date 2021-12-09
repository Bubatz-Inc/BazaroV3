using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class TagGroup
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
