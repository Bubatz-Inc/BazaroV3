using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.CalendarEntries
{
    public static class UpdateCalendarEntry
    {
        public class Command
        {
            public int CalendarEntryId { get; set; }

            public int EntryId { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        /// <summary>
        /// Updates CalendarEntry
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<CalendarEntry>().FirstOrDefaultAsync(x => x.Id == request.CalendarEntryId);

            if (data == null)
                return;

            data.EntryId = request.EntryId;
            data.StartDate = request.StartDate;
            data.EndDate = request.EndDate;

            await context.SaveChangesAsync();
        }
    }
}
