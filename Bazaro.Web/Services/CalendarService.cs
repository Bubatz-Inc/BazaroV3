using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services
{
    public class CalendarService
    {
        private readonly BazaroContext _context;

        public CalendarService(BazaroContext context)
        {
            _context = context;
        }

        public async Task<List<CalendarEntry>> GetCalendarEntries(DateTime startDate, DateTime endDate, User user)
        {
            return await _context.Set<UserFolderReference>()
                .Join(_context.Set<FolderEntryReference>(),
                    userFolder => userFolder.FolderId,
                    folderEntry => folderEntry.FolderId,
                    (userFolder, folderEntry) => new { userFolder, folderEntry })
                .Join(_context.Set<CalendarEntry>(),
                    combined => combined.folderEntry.EntryId,
                    calendarEntry => calendarEntry.EntryId,
                    (combined, calendarEntry) => new {calendarEntry, UserId = combined.userFolder.UserId})
                .Where(x => x.UserId == user.Id)
                .Where(x => x.calendarEntry.StartDate >= startDate && x.calendarEntry.EndDate <= endDate)
                .Select(x => x.calendarEntry)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Id of inserted entry</returns>
        public async Task<int> AddCalendarEntry(Entry entry, DateTime startDate, DateTime? endDate = null)
        {
            var calendarEntry = new CalendarEntry {
                Entry = entry,
                EntryId = entry.Id,
                StartDate = startDate,
                EndDate = endDate,
            };

            _context.Add(calendarEntry);
            await _context.SaveChangesAsync();
            return calendarEntry.Id;
        }

        public Task DeleteCalendarEntry(CalendarEntry centry)
        {
            _context.Remove(centry);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteCalendarEntry(int centryId)
        {
            var found = _context.Set<CalendarEntry>().FirstOrDefaultAsync(x => x.Id == centryId);
            if (found is null) return;
            _context.Remove(found);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCalendarEntry(CalendarEntry centry)
        {
            var found = _context.Set<CalendarEntry>().FirstOrDefaultAsync(x => x.Id == centry.Id);
            if (found is null) return;
            _context.Remove(found);
            await _context.SaveChangesAsync();
        }
    }
}
