using Bazaro.Core.Services.Models;
using Bazaro.Data;
using Bazaro.Data.Models;
using Bazaro.Data.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Core.Services.Queries.Entries
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
                    Tags = entry.EntryTag.Select(x => new EntryTagModel
                    {
                        Id = x.Id,
                        Title = x.Title
                    }).ToList(),
                    StartItem = CreateEntryModel(entry.StartItem)
                };
            }

            return result;
        }

        private static ItemModel? CreateEntryModel(Item? item)
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
