using Bazaro.Web.Models.References;
using Bazaro.Web.Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bazaro.Web.Services.Queries.EntryReferences
{
    public static class GetEntryReferencesByEntryId
    {
        public class Query
        {
            public int EntryId { get; set; }
        }

        public static Task<List<EntryReferenceViewModel>> Handle(BazaroContext context, Query request)
        {
            return context.Set<EntryReference>()
                .Where(x => x.EntryId == request.EntryId)
                .Select(x => new EntryReferenceViewModel
                {
                    EntityId = x.EntryId,
                    ReferenceEntityId = x.ReferenceEntryId,
                    ReferenceEntityTitle = x.ReferenceEntry.Title
                }).ToListAsync();
        }
    }
}
