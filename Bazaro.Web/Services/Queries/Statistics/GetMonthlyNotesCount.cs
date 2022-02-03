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

        /// <summary>
        /// Returns Monthly State by userId
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns>List of MonthlyStateModel</returns>
        public static async Task<List<MonthlyStateModel>> Handle(BazaroContext context, Query request)
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
                .Select(x => new MonthlyStateModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Count()
                }).ToList();
        }
    }
}
