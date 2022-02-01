using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Entries
{
    public static class GetEntriesByTitle
    {
        public class Query
        {
            public string Title { get; set; }
            public string UserId { get; set; }
            public int MaxCount { get; set; }
        }

        public static Task<List<EntryModel>> Handler(BazaroContext context, Query request)
        {
            return context.Set<Entry>()
                .Where(x => x.Title.ToLower().Contains(request.Title.ToLower())
                    || request.Title.ToLower().Contains(x.Title))
                .Join(context.Set<FolderEntryReference>(),
                    e => e.Id,
                    fe => fe.EntryId,
                    (e, fe) => new { e, fe})
                .Join(context.Set<UserFolderReference>(),
                    efe => efe.fe.FolderId,
                    uf => uf.FolderId,
                    (efe, uf) => new { efe, uf })
                .Where(x => x.uf.UserId == request.UserId)
                .Select( x => x.efe.e)
                .Take(request.MaxCount)
                .Select(x => new EntryModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Title = x.Title,
                    StartItemId = x.StartItemId
                }).ToListAsync();
        }
    }
}
