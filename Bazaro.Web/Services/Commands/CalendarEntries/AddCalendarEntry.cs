using Bazaro.Web.Models;

namespace Bazaro.Web.Services.Commands.CalendarEntries
{
    public static class AddCalendarEntry
    {
        public class Command
        {
            public int EntryId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public static Task Handle(BazaroContext context, Command request)
        {
            context.Add(new CalendarEntry
            {
                EntryId = request.EntryId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Created = DateTime.Now,
            });

            return context.SaveChangesAsync();
        }
    }
}
