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
            var userFolders = await context.Set<UserFolderReference>()
                .Where(x => x.UserId == request.UserId)
                .ToListAsync();

            var entries = new List<EntryModel>();
            foreach (var item in userFolders)
            {
                // TOFIX: selects all notes instead of just the notes in the folder
                entries.AddRange(await context.Set<FolderEntryReference>()
                    .Include(x => x.Entry)
                    .Where(x => x.FolderId == item.FolderId)
                    .Select(x => new EntryModel
                    {
                        Id = x.Entry.Id,
                        Title = x.Entry.Title,
                        Description = x.Entry.Description,
                        StartItemId = x.Entry.StartItemId,
                    }).ToListAsync());
            }

            return entries;
        }
    }
}
