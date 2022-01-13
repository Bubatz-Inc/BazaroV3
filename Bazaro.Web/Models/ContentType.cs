using Bazaro.Web.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    public record class ContentType : IdEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
