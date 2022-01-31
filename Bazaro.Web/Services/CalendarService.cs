using Bazaro.Web.Services.Commands.CalendarEntries;
using Bazaro.Web.Services.Queries.CalendarEntries;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services
{
    public class CalendarService
    {
        private readonly BazaroContext _context;

        public CalendarService(BazaroContext context)
        {
            _context = context;
        }

        public Task<List<CalendarEntryModel>> GetCalendarEntries(GetCalenderEntriesByUserId.Query request) => GetCalenderEntriesByUserId.Handle(_context, request);

        public Task AddCalendarEntry(AddCalendarEntry.Command request) => Commands.CalendarEntries.AddCalendarEntry.Handle(_context, request);

        public async Task DeleteCalendarEntry(DeleteCalendarEntryById.Command request) => await DeleteCalendarEntryById.Handle(_context, request);

        public async Task UpdateCalendarEntry(UpdateCalendarEntry.Command request) => await Commands.CalendarEntries.UpdateCalendarEntry.Handle(_context, request);
    }
}
