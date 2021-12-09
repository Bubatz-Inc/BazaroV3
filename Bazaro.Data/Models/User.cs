using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class User : IdEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] Password { get; set; }
        public string AvatarUrl { get; set; }
    }
}
