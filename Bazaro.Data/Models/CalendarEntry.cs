using Bazaro.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Data.Models
{
    public record class CalendarEntry : IdEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public Tag[] Tags { get; set; }
    }
}
