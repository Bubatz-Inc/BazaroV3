using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.CalendarEntries
{
    public static class GetCalenderEntriesByUserId
    {
        public class Query
        {
            public string UserId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        /// <summary>
        /// Returns Calendar Entries by userId
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns>List of CalendarEntryModel</returns>
        public static async Task<List<CalendarEntryModel>> Handle(BazaroContext context, Query request)
        {
            var res = await context.Set<UserFolderReference>()
                .Join(context.Set<FolderEntryReference>(),
                    uf => uf.FolderId,
                    ue => ue.FolderId,
                    (uf, ue) => new { uf, ue })
                .Join(context.Set<CalendarEntry>(),
                    x => x.ue.EntryId,
                    ce => ce.EntryId,
                    (x, ce) => new { x.ue, x.uf, ce })
                .Where(x => x.uf.UserId == request.UserId
                    && x.ce.StartDate >= request.StartDate && x.ce.StartDate <= request.EndDate)
                .Select(x => new CalendarEntryModel
                {
                    Id = x.ce.Id,
                    StartDate = x.ce.StartDate,
                    EndDate = x.ce.EndDate,
                    Entry = new EntryModel
                    {
                        Id = x.ce.Id,
                        Title = x.ce.Entry.Title,
                        Description = x.ce.Entry.Description,
                        StartItemId = x.ce.Entry.StartItemId
                    }
                }).ToListAsync();

            return res;
        }
    }
}
