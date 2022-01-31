using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Commands.CalendarEntries
{
    public static class DeleteCalendarEntryById
    {
        public class Command
        {
            public int CalendarEntryId { get; set; }
        }

        public static async Task Handle(BazaroContext context, Command request)
        {
            var data = await context.Set<CalendarEntry>().FirstOrDefaultAsync(x => x.Id == request.CalendarEntryId);

            if (data == null)
                return;

            context.Remove(data);

            await context.SaveChangesAsync();
        }
    }
}
