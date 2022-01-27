using Bazaro.Web.Models.References;
using Bazaro.Web.Models;
using Microsoft.EntityFrameworkCore;
using Bazaro.Web.Services.ViewModels;

namespace Bazaro.Web.Services.Queries.Statistics
{
    public static class GetMonthlyNotesCount
    {
        public class Query
        {
            public string UserId { get; set; }
        }

        public static async Task<List<MonthlyStatViewModel>> Handle(BazaroContext context, Query request)
        {
            var folders = await context.Set<UserFolderReference>().Where(x => x.UserId == request.UserId).ToListAsync();

            if(folders == null)
                return null;

            List<Entry> entries = new List<Entry>();
            foreach(var folder in folders)
            {
                entries.AddRange(await context.Set<FolderEntryReference>().Where(x => x.FolderId == folder.FolderId).Select(x => x.Entry).ToListAsync());
            }

            return entries.GroupBy(x => new { x.Created.Month, x.Created.Year })
                .Select(x => new MonthlyStatViewModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Count()
                }).ToList();
        }
    }
}
