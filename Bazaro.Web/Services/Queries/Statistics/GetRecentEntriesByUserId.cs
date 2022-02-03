using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Statistics
{
    public static class GetRecentEntriesByUserId
    {
        public class Query
        {
            public string UserId { get; set; }
            public int MaxCount { get; set; }
        }

        public static Task<List<EntryModel>> Handle(BazaroContext context, Query request)
        {
            return context.Set<UserFolderReference>()
                .Join(context.Set<FolderEntryReference>()
                    .Include(x => x.Entry),
                ufr => ufr.FolderId,
                fer => fer.FolderId,
                (ufr, fer) => new { ufr, fer })
                .OrderByDescending(x => x.fer.Entry.Updated)
                .Take(request.MaxCount)
                .Select(x => new EntryModel
                {
                    Id = x.fer.Entry.Id,
                    Description = x.fer.Entry.Description,
                    Title = x.fer.Entry.Title,
                    StartItemId = x.fer.Entry.StartItemId
                }).ToListAsync();
        }
    }
}
