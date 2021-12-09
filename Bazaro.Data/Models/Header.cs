using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class Header : IdEntity
    {
        [Required]
        // TODO: add Maxlength
        public byte[] Content { get; set; }

        [Required]
        public HeaderType Type { get; set; }
    }
}
