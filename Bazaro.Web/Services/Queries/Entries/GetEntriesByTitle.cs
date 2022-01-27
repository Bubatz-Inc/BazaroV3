using Bazaro.Web.Models;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Entries
{
    public static class GetEntriesByTitle
    {
        public class Query
        {
            public string Title { get; set; }
            public int MaxCount { get; set; }
        }

        public static Task<List<EntryModel>> Handler(BazaroContext context, Query request)
        {
            return context.Set<Entry>()
                .Where(x => x.Title.ToLower().Contains(request.Title.ToLower())
                    || request.Title.ToLower().Contains(x.Title))
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
