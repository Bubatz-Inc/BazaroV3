using Bazaro.Web.Models.Base;

namespace Bazaro.Web.Models
{
    public record class CalendarEntry: IdEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Entry Entry { get; set; }
        public int EntryId { get; set; }
    }
}
