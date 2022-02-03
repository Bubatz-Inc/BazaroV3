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

        public Task Insert(InsertCalendarEntry.Command request) => Commands.CalendarEntries.InsertCalendarEntry.Handle(_context, request);

        public async Task Delete(DeleteCalendarEntryById.Command request) => await DeleteCalendarEntryById.Handle(_context, request);

        public async Task Update(UpdateCalendarEntry.Command request) => await Commands.CalendarEntries.UpdateCalendarEntry.Handle(_context, request);
    }
}
