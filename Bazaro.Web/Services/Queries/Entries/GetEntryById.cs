using Bazaro.Web.Models;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.Entries
{
    public static class GetEntryById
    {
        public class Query
        {
            public int Id { get; set; }
        }

        public static Task<EntryModel> Handle(BazaroContext context, Query request)
        {
            return context.Set<Entry>()
                .Select(x => new EntryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    StartItemId = x.StartItemId,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
