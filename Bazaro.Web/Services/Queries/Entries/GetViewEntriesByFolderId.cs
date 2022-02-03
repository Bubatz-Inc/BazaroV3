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

        public static async Task<List<EntryModel>> Handle(BazaroContext context, Query request)
        {
            var userFolderReferences = await context.Set<UserFolderReference>()
                .Join(context.Set<FolderEntryReference>()
                        .Include(x => x.Entry),
                    ufr => ufr.FolderId,
                    fer => fer.FolderId,
                    (ufr, fer) => new { ufr, fer })
                .Where(x => x.ufr.UserId == request.UserId && x.ufr.FolderId == request.FolderId)
                .Select(x => x.fer)
                .ToListAsync();

            var entryList = new List<EntryModel>();
            foreach (var item in userFolderReferences)
            {
                if (entryList.Any(x => x.Id == item.Entry.Id))
                    continue;

                entryList.Add(new EntryModel
                {
                    Id = item.Entry.Id,
                    Description = item.Entry.Description,
                    Title = item.Entry.Title,
                    StartItemId = item.Entry.StartItemId,
                });
            }

            return entryList;
        }
    }
}
