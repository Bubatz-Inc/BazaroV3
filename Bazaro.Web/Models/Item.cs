using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    public record class Item : IdEntity
    {
        public Item NextItem { get; set; }
        public int NextItemId { get; set; }

        [Required]
        public ContentType ContentType { get; set; }
        public int ContentTypeId { get; set; }

        public byte[] Content { get; set; }
    }
}
