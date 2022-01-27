using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Entries
{
    public static class GetViewEntriesByFolderId
    {
        public class Query
        {
            public string UserId { get; set; }
            public int? FolderId { get; set; }
        }

        public static Task<List<EntryModel>> Handle(BazaroContext context, Query request)
        {
            return context.Set<FolderEntryReference>()
                .Include(x => x.Entry)
                .Join(context.Set<UserFolderReference>(),
                    fer => fer.FolderId,
                    ufr => ufr.FolderId,
                    (fer, ufr) => new { fer, ufr })
                .Where(x => x.ufr.UserId == request.UserId && x.ufr.UserId == request.UserId)
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
