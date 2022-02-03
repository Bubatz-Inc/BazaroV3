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

        /// <summary>
        /// Returns Entry References by EntryId
        /// </summary>
        /// <param name="context">Database-Context</param>
        /// <param name="request">Request-Data</param>
        /// <returns>List of EntryReferenceModel</returns>
        public static Task<List<EntryReferenceModel>> Handle(BazaroContext context, Query request)
        {
            return context.Set<EntryReference>()
                .Where(x => x.EntryId == request.EntryId)
                .Select(x => new EntryReferenceModel
                {
                    EntityId = x.EntryId,
                    ReferenceEntityId = x.ReferenceEntryId,
                    ReferenceEntityTitle = x.ReferenceEntry.Title
                }).ToListAsync();
        }
    }
}
