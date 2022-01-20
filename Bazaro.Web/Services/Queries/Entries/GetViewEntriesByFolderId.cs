using Bazaro.Web.Models;
using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Entries
{
    public static class GetViewEntriesByFolderId
    {
        public class Query
        {
            public int FolderId { get; set; }
        }

        public static async Task<List<EntryModel>> Handle(BazaroContext context, Query request)
        {
            var entryList = await context.Set<FolderEntryReference>()
                .Include(x => x.Entry)
                .ThenInclude(x => x.StartItem)
                .ThenInclude(x => x.NextItem)
                .Where(x => x.FolderId == request.FolderId)
                .Select(x => x.Entry)
                .ToListAsync();

            var result = new List<EntryModel>();
            foreach (var entry in entryList)
            {
                var model = new EntryModel
                {
                    Id = entry.Id,
                    Title = entry.Title,
                    Description = entry.Description,
                    StartItemId = entry.StartItemId
                };
            }

            return result;
        }

        private static ItemModel CreateEntryModel(Item item)
        {
            if (item == null)
                return null;

            return new ItemModel
            {
                ContentType = item.ContentType.Title,
                Content = item.Content,
                NextItem = CreateEntryModel(item.NextItem)
            };
        }
    }
}
